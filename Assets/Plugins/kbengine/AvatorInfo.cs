namespace KBEngine
{
    using UnityEngine;
    using System.Collections;
    using System;

    public class AvatorInfo
    {

        public static AvatorInfo inst = null;
        public string name;
        public UInt64 exp;
        public UInt16 level;
        public Int32 hpmax;
        public Int32 mpmax;
        public Int32 hp;
        public Int32 mp;
        public UInt32 phattack;
        public UInt32 magicattack;
        public UInt32 phdef;
        public UInt32 madef;
        public AvatorInfo()
        {
            inst = this;
        }
    }
}
