// This code has been based from the sample repository "cecil": https://github.com/jbevain/cecil
// Copyright (c) 2020 - 2021 Faber Leonardo. All Rights Reserved. https://github.com/FaberSanZ
// This code is licensed under the MIT license (MIT) (http://opensource.org/licenses/MIT)


namespace LSharp.IL.Metadata
{

    public sealed class UserStringHeap : StringHeap
    {

        public UserStringHeap(byte[] data)
            : base(data)
        {
        }

        protected override string ReadStringAt(uint index)
        {
            int start = (int)index;

            uint length = (uint)(data.ReadCompressedUInt32(ref start) & ~1);
            if (length < 1)
            {
                return string.Empty;
            }

            char[] chars = new char[length / 2];

            for (int i = start, j = 0; i < start + length; i += 2)
            {
                chars[j++] = (char)(data[i] | (data[i + 1] << 8));
            }

            return new string(chars);
        }
    }
}
