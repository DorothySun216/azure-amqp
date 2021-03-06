﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace Microsoft.Azure.Amqp.Framing
{
    using System.Text;

    /// <summary>
    /// Defines the header section of a message.
    /// </summary>
    public sealed class Header : DescribedList
    {
        /// <summary>Descriptor name.</summary>
        public static readonly string Name = "amqp:header:list";
        /// <summary>Descriptor name.</summary>
        public static readonly ulong Code = 0x0000000000000070;
        const int Fields = 5;

        /// <summary>
        /// Initializes the object.
        /// </summary>
        public Header() : base(Name, Code)
        {
        }

        /// <summary>
        /// Gets or sets the "durable" field.
        /// </summary>
        public bool? Durable { get; set; }

        /// <summary>
        /// Gets or sets the "priority" field.
        /// </summary>
        public byte? Priority { get; set; }

        /// <summary>
        /// Gets or sets the "ttl" field.
        /// </summary>
        public uint? Ttl { get; set; }

        /// <summary>
        /// Gets or sets the "first-acquirer" field.
        /// </summary>
        public bool? FirstAcquirer { get; set; }

        /// <summary>
        /// Gets or sets the "delivery-count" field.
        /// </summary>
        public uint? DeliveryCount { get; set; }

        internal override int FieldCount
        {
            get { return Fields; }
        }

        /// <summary>
        /// Returns a string that represents the object.
        /// </summary>
        /// <returns>The string representation.</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("header(");
            int count = 0;
            this.AddFieldToString(this.Durable != null, sb, "durable", this.Durable, ref count);
            this.AddFieldToString(this.Priority != null, sb, "priority", this.Priority, ref count);
            this.AddFieldToString(this.Ttl != null, sb, "ttl", this.Ttl, ref count);
            this.AddFieldToString(this.FirstAcquirer != null, sb, "first-acquirer", this.FirstAcquirer, ref count);
            this.AddFieldToString(this.DeliveryCount != null, sb, "delivery-count", this.DeliveryCount, ref count);
            sb.Append(')');
            return sb.ToString();
        }

        internal override void OnEncode(ByteBuffer buffer)
        {
            AmqpCodec.EncodeBoolean(this.Durable, buffer);
            AmqpCodec.EncodeUByte(this.Priority, buffer);
            AmqpCodec.EncodeUInt(this.Ttl, buffer);
            AmqpCodec.EncodeBoolean(this.FirstAcquirer, buffer);
            AmqpCodec.EncodeUInt(this.DeliveryCount, buffer);
        }

        internal override void OnDecode(ByteBuffer buffer, int count)
        {
            if (count-- > 0)
            {
                this.Durable = AmqpCodec.DecodeBoolean(buffer);
            }

            if (count-- > 0)
            {
                this.Priority = AmqpCodec.DecodeUByte(buffer);
            }

            if (count-- > 0)
            {
                this.Ttl = AmqpCodec.DecodeUInt(buffer);
            }

            if (count-- > 0)
            {
                this.FirstAcquirer = AmqpCodec.DecodeBoolean(buffer);
            }

            if (count-- > 0)
            {
                this.DeliveryCount = AmqpCodec.DecodeUInt(buffer);
            }
        }

        internal override int OnValueSize()
        {
            int valueSize = 0;

            valueSize = AmqpCodec.GetBooleanEncodeSize(this.Durable);
            valueSize += AmqpCodec.GetUByteEncodeSize(this.Priority);
            valueSize += AmqpCodec.GetUIntEncodeSize(this.Ttl);
            valueSize += AmqpCodec.GetBooleanEncodeSize(this.FirstAcquirer);
            valueSize += AmqpCodec.GetUIntEncodeSize(this.DeliveryCount);

            return valueSize;
        }
    }
}
