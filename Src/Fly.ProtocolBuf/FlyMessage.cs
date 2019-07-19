// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: FlyMessage.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace Fly.ProtocolBuf {

  /// <summary>Holder for reflection information generated from FlyMessage.proto</summary>
  public static partial class FlyMessageReflection {

    #region Descriptor
    /// <summary>File descriptor for FlyMessage.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static FlyMessageReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChBGbHlNZXNzYWdlLnByb3RvEg9GbHkuUHJvdG9jb2xCdWYizwEKC0NoYXRN",
            "ZXNzYWdlEg4KBlNvdXJjZRgBIAEoCRIOCgZUYXJnZXQYAiABKAkSDwoHQ29u",
            "dGVudBgDIAEoCRI6CgtDb250ZW50VHlwZRgEIAEoDjIlLkZseS5Qcm90b2Nv",
            "bEJ1Zi5DaGF0TWVzc2FnZS5DaGF0VHlwZSJTCghDaGF0VHlwZRIICgRUZXh0",
            "EAASCQoFSW1hZ2UQARIJCgVBdWRpbxACEgkKBVZpZGVvEAMSDQoJVmlkZW9D",
            "YWxsEAQSDQoJQXVkaW9DYWxsEAUihQEKDUNvbW1vbk1lc3NhZ2USDgoGU291",
            "cmNlGAEgASgJEjsKB01lc3NhZ2UYAiABKA4yKi5GbHkuUHJvdG9jb2xCdWYu",
            "Q29tbW9uTWVzc2FnZS5NZXNzYWdlVHlwZSInCgtNZXNzYWdlVHlwZRIJCgVM",
            "b2dpbhAAEg0KCUhlYXJ0QmVhdBABQh8KE2NvbS5nb29nbGUucHJvdG9idWZC",
            "CEFueVByb3RvYgZwcm90bzM="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::Fly.ProtocolBuf.ChatMessage), global::Fly.ProtocolBuf.ChatMessage.Parser, new[]{ "Source", "Target", "Content", "ContentType" }, null, new[]{ typeof(global::Fly.ProtocolBuf.ChatMessage.Types.ChatType) }, null, null),
            new pbr::GeneratedClrTypeInfo(typeof(global::Fly.ProtocolBuf.CommonMessage), global::Fly.ProtocolBuf.CommonMessage.Parser, new[]{ "Source", "Message" }, null, new[]{ typeof(global::Fly.ProtocolBuf.CommonMessage.Types.MessageType) }, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  /// <summary>
  ///聊天消息
  /// </summary>
  public sealed partial class ChatMessage : pb::IMessage<ChatMessage> {
    private static readonly pb::MessageParser<ChatMessage> _parser = new pb::MessageParser<ChatMessage>(() => new ChatMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<ChatMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Fly.ProtocolBuf.FlyMessageReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessage(ChatMessage other) : this() {
      source_ = other.source_;
      target_ = other.target_;
      content_ = other.content_;
      contentType_ = other.contentType_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public ChatMessage Clone() {
      return new ChatMessage(this);
    }

    /// <summary>Field number for the "Source" field.</summary>
    public const int SourceFieldNumber = 1;
    private string source_ = "";
    /// <summary>
    ///发送端
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Source {
      get { return source_; }
      set {
        source_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Target" field.</summary>
    public const int TargetFieldNumber = 2;
    private string target_ = "";
    /// <summary>
    ///对端
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Target {
      get { return target_; }
      set {
        target_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Content" field.</summary>
    public const int ContentFieldNumber = 3;
    private string content_ = "";
    /// <summary>
    ///内容
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Content {
      get { return content_; }
      set {
        content_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "ContentType" field.</summary>
    public const int ContentTypeFieldNumber = 4;
    private global::Fly.ProtocolBuf.ChatMessage.Types.ChatType contentType_ = global::Fly.ProtocolBuf.ChatMessage.Types.ChatType.Text;
    /// <summary>
    ///内容类型
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Fly.ProtocolBuf.ChatMessage.Types.ChatType ContentType {
      get { return contentType_; }
      set {
        contentType_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as ChatMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(ChatMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Source != other.Source) return false;
      if (Target != other.Target) return false;
      if (Content != other.Content) return false;
      if (ContentType != other.ContentType) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Source.Length != 0) hash ^= Source.GetHashCode();
      if (Target.Length != 0) hash ^= Target.GetHashCode();
      if (Content.Length != 0) hash ^= Content.GetHashCode();
      if (ContentType != global::Fly.ProtocolBuf.ChatMessage.Types.ChatType.Text) hash ^= ContentType.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Source.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Source);
      }
      if (Target.Length != 0) {
        output.WriteRawTag(18);
        output.WriteString(Target);
      }
      if (Content.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Content);
      }
      if (ContentType != global::Fly.ProtocolBuf.ChatMessage.Types.ChatType.Text) {
        output.WriteRawTag(32);
        output.WriteEnum((int) ContentType);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Source.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Source);
      }
      if (Target.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Target);
      }
      if (Content.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Content);
      }
      if (ContentType != global::Fly.ProtocolBuf.ChatMessage.Types.ChatType.Text) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) ContentType);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(ChatMessage other) {
      if (other == null) {
        return;
      }
      if (other.Source.Length != 0) {
        Source = other.Source;
      }
      if (other.Target.Length != 0) {
        Target = other.Target;
      }
      if (other.Content.Length != 0) {
        Content = other.Content;
      }
      if (other.ContentType != global::Fly.ProtocolBuf.ChatMessage.Types.ChatType.Text) {
        ContentType = other.ContentType;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Source = input.ReadString();
            break;
          }
          case 18: {
            Target = input.ReadString();
            break;
          }
          case 26: {
            Content = input.ReadString();
            break;
          }
          case 32: {
            ContentType = (global::Fly.ProtocolBuf.ChatMessage.Types.ChatType) input.ReadEnum();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the ChatMessage message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum ChatType {
        /// <summary>
        ///文字
        /// </summary>
        [pbr::OriginalName("Text")] Text = 0,
        /// <summary>
        ///图像
        /// </summary>
        [pbr::OriginalName("Image")] Image = 1,
        /// <summary>
        ///音频
        /// </summary>
        [pbr::OriginalName("Audio")] Audio = 2,
        /// <summary>
        ///视频
        /// </summary>
        [pbr::OriginalName("Video")] Video = 3,
        /// <summary>
        ///音频通话
        /// </summary>
        [pbr::OriginalName("VideoCall")] VideoCall = 4,
        /// <summary>
        ///视频通话
        /// </summary>
        [pbr::OriginalName("AudioCall")] AudioCall = 5,
      }

    }
    #endregion

  }

  /// <summary>
  ///普通消息
  /// </summary>
  public sealed partial class CommonMessage : pb::IMessage<CommonMessage> {
    private static readonly pb::MessageParser<CommonMessage> _parser = new pb::MessageParser<CommonMessage>(() => new CommonMessage());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<CommonMessage> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::Fly.ProtocolBuf.FlyMessageReflection.Descriptor.MessageTypes[1]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonMessage() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonMessage(CommonMessage other) : this() {
      source_ = other.source_;
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public CommonMessage Clone() {
      return new CommonMessage(this);
    }

    /// <summary>Field number for the "Source" field.</summary>
    public const int SourceFieldNumber = 1;
    private string source_ = "";
    /// <summary>
    /// 发送端
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Source {
      get { return source_; }
      set {
        source_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    /// <summary>Field number for the "Message" field.</summary>
    public const int MessageFieldNumber = 2;
    private global::Fly.ProtocolBuf.CommonMessage.Types.MessageType message_ = global::Fly.ProtocolBuf.CommonMessage.Types.MessageType.Login;
    /// <summary>
    /// 消息类型
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public global::Fly.ProtocolBuf.CommonMessage.Types.MessageType Message {
      get { return message_; }
      set {
        message_ = value;
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as CommonMessage);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(CommonMessage other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Source != other.Source) return false;
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Source.Length != 0) hash ^= Source.GetHashCode();
      if (Message != global::Fly.ProtocolBuf.CommonMessage.Types.MessageType.Login) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
      if (Source.Length != 0) {
        output.WriteRawTag(10);
        output.WriteString(Source);
      }
      if (Message != global::Fly.ProtocolBuf.CommonMessage.Types.MessageType.Login) {
        output.WriteRawTag(16);
        output.WriteEnum((int) Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Source.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Source);
      }
      if (Message != global::Fly.ProtocolBuf.CommonMessage.Types.MessageType.Login) {
        size += 1 + pb::CodedOutputStream.ComputeEnumSize((int) Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(CommonMessage other) {
      if (other == null) {
        return;
      }
      if (other.Source.Length != 0) {
        Source = other.Source;
      }
      if (other.Message != global::Fly.ProtocolBuf.CommonMessage.Types.MessageType.Login) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 10: {
            Source = input.ReadString();
            break;
          }
          case 16: {
            Message = (global::Fly.ProtocolBuf.CommonMessage.Types.MessageType) input.ReadEnum();
            break;
          }
        }
      }
    }

    #region Nested types
    /// <summary>Container for nested types declared in the CommonMessage message type.</summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static partial class Types {
      public enum MessageType {
        [pbr::OriginalName("Login")] Login = 0,
        [pbr::OriginalName("HeartBeat")] HeartBeat = 1,
      }

    }
    #endregion

  }

  #endregion

}

#endregion Designer generated code