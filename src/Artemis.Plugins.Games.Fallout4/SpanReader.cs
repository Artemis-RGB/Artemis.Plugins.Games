using System;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Text;

namespace Artemis.Plugins.Games.Fallout4
{
    internal ref struct SpanReader
    {
        private readonly Span<byte> _data;
        private int _position;
        public bool End => _position >= _data.Length;

        public SpanReader(Span<byte> d)
        {
            _data = d;
            _position = 0;
        }

        public bool ReadBoolean()
        {
            return ReadByte() == 1;
        }

        public sbyte ReadSByte()
        {
            return unchecked((sbyte)ReadByte());
        }

        public byte ReadByte()
        {
            if (_position + sizeof(byte) > _data.Length)
                throw new Exception();

            var ret =  _data[_position];
            _position += sizeof(byte);
            return ret;
        }

        public ushort ReadUInt16()
        {
            if (_position + sizeof(ushort) > _data.Length)
                throw new Exception();

            var ret = BinaryPrimitives.ReadUInt16LittleEndian(_data[_position..]);
            _position += sizeof(ushort);
            return ret;
        }

        public int ReadInt32()
        {
            if (_position + sizeof(int) > _data.Length)
                throw new Exception();

            var ret = BinaryPrimitives.ReadInt32LittleEndian(_data[_position..]);
            _position += sizeof(int);
            return ret;
        }

        public uint ReadUInt32()
        {
            if (_position + sizeof(uint) > _data.Length)
                throw new Exception();

            var ret = BinaryPrimitives.ReadUInt32LittleEndian(_data[_position..]);
            _position += sizeof(uint);
            return ret;
        }

        public float ReadSingle()
        {
            if (_position + sizeof(float) > _data.Length)
                throw new Exception();

            var ret = BinaryPrimitives.ReadSingleLittleEndian(_data[_position..]);
            _position += sizeof(float);
            return ret;
        }
        
        public string ReadNullTerminatedString()
        {
            StringBuilder builder = new();
            char c;

            while ((c = (char)ReadByte()) != char.MinValue)
                builder.Append(c);

            return builder.ToString();
        }

        public uint[] ReadArray()
        {
            var arraySize = ReadUInt16();
            var array = new uint[arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                array[i] = ReadUInt32();
            }

            return array;
        }

        public SortedDictionary<uint, string> ReadMap()
        {
            SortedDictionary<uint, string> dict = new();

            var dictSize = ReadUInt16();
            for (int i = 0; i < dictSize; i++)
            {
                var key = ReadUInt32();
                var value = ReadNullTerminatedString();

                dict[key] = value;
            }
            return dict;
        }
    }
}
