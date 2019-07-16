using System;

namespace Fly.Handler.Channels
{
    public class HostInfo:IEquatable<HostInfo>
    {
        public string Address { get; }

        public int Port { get; }

        public string Description { get; }

        public HostInfo(string address, int port, string description)
        {
            Address = address;
            Port = port;
            Description = description;
        }

        public override string ToString()
        {
            var description = string.IsNullOrEmpty(Description) ? string.Empty : $"({Description})";
            return $"{Address}:{Port}{description}";
        }

        public bool Equals(HostInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Address, other.Address) && Port == other.Port && string.Equals(Description, other.Description);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((HostInfo) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = (Address != null ? Address.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Port;
                hashCode = (hashCode * 397) ^ (Description != null ? Description.GetHashCode() : 0);
                return hashCode;
            }
        }

        public static bool operator ==(HostInfo left, HostInfo right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(HostInfo left, HostInfo right)
        {
            return !Equals(left, right);
        }
    }
}
