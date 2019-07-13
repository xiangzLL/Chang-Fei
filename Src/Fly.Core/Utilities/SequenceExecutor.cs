using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace Fly.Core.Utilities
{
    public class ExecuteWorker<T> where T : class
    {
        public T Parameter { get; }

        private readonly Func<T, bool> _executeFunc;
        private readonly Action<T> _executeAction;

        public ExecuteWorker(Func<T, bool> executeFunc, T parameter)
        {
            Parameter = parameter;
            _executeFunc = executeFunc;
        }

        public ExecuteWorker(Action<T> executeAction, T parameter)
        {
            Parameter = parameter;
            _executeAction = executeAction;
        }

        public bool Run()
        {
            if (_executeFunc != null)
            {
                return _executeFunc(Parameter);
            }

            _executeAction?.Invoke(Parameter);

            return true;
        }
    }

    /// <summary>
    /// 消息执行队列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SequenceExecutor<T> where T : class
    {
        private readonly int _handleThreadCounts = 1;
        private readonly TimeSpan _executeDelayTime = TimeSpan.MinValue;
        private readonly BlockingCollection<ExecuteWorker<T>> _blockingCollection =
            new BlockingCollection<ExecuteWorker<T>>();
        //0 未暂停，1暂停
        private int _isPaused;

        /// <summary>
        /// 队列名称
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 长度
        /// </summary>
        public int Count => _blockingCollection.Count;

        /// <summary>
        /// 消息队列是否暂停
        /// </summary>
        public bool IsPaused => _isPaused != 0;

        /// <summary>
        /// 消息执行队列
        /// </summary>
        /// <param name="name">队列名称</param>
        public SequenceExecutor(string name)
        {
            Name = name;
            InitializeConsumeThreads();
        }

        /// <summary>
        /// 消息执行队列
        /// </summary>
        /// <param name="name">队列名称</param>
        /// <param name="executeDelayTime">每次执行完毕，延迟时间</param>
        public SequenceExecutor(string name, TimeSpan executeDelayTime)
        {
            Name = name;
            _executeDelayTime = executeDelayTime;
            InitializeConsumeThreads();
        }

        /// <summary>
        /// 消息执行队列
        /// </summary>
        /// <param name="name">队列名称</param>
        /// <param name="handleThreadCounts">并行执行数</param>
        public SequenceExecutor(string name, int handleThreadCounts)
        {
            Name = name;
            _handleThreadCounts = handleThreadCounts;
            InitializeConsumeThreads();
        }

        /// <summary>
        /// 消息执行队列
        /// </summary>
        /// <param name="name">队列名称</param>
        /// <param name="handleThreadCounts">并行执行数</param>
        /// <param name="executeDelayTime">每次执行完毕，延迟时间</param>
        public SequenceExecutor(string name, int handleThreadCounts, TimeSpan executeDelayTime)
        {
            Name = name;
            _handleThreadCounts = handleThreadCounts;
            _executeDelayTime = executeDelayTime;
            InitializeConsumeThreads();
        }

        private void InitializeConsumeThreads()
        {
            for (var i = 0; i < _handleThreadCounts; i++)
            {
                Task.Run(() =>
                {
                    while (!_blockingCollection.IsCompleted)
                    {
                        while (!IsPaused)
                        {
                            try
                            {
                                if (_blockingCollection.TryTake(out var worker))
                                {
                                    if (worker != null)
                                    {
                                        var result = worker.Run();
                                        if (!result)
                                        {
                                            Interlocked.CompareExchange(ref _isPaused, 1, 0);
                                        }
                                        if (_executeDelayTime != TimeSpan.MinValue)
                                        {
                                            Thread.SpinWait(_executeDelayTime.Milliseconds);
                                        }
                                    }
                                }
                            }
                            catch
                            {
                                //ignore
                            }
                        }
                    }
                });
            }
        }

        /// <summary>
        /// 恢复消息队列执行
        /// </summary>
        public void Resume()
        {
            Interlocked.CompareExchange(ref _isPaused, 0, 1);
        }

        /// <summary>
        /// 添加执行方法
        /// </summary>
        /// <param name="action"></param>
        /// <param name="paramater"></param>
        /// <returns></returns>
        public bool Add(Action<T> action, T paramater)
        {
            var executeWorker = new ExecuteWorker<T>(action, paramater);
            return _blockingCollection.TryAdd(executeWorker);
        }

        /// <summary>
        /// 添加状态执行方法
        /// </summary>
        /// <param name="func">如果返回值为false，暂停队列</param>
        /// <param name="paramater"></param>
        /// <returns></returns>
        public bool AddWithStatus(Func<T, bool> func, T paramater)
        {
            var executeWorker = new ExecuteWorker<T>(func, paramater);
            return _blockingCollection.TryAdd(executeWorker);
        }

        /// <summary>
        /// 结束消息执行队列
        /// </summary>
        private void Close()
        {
            _blockingCollection.CompleteAdding();
            _blockingCollection.Dispose();
        }
    }
}
