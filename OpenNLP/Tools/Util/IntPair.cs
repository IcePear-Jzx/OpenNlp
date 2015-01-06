﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenNLP.Tools.Util
{
    public class IntPair : IntTuple
    {
        private static readonly long serialVersionUID = 1L;


        public IntPair() : base(2)
        {
        }

        public IntPair(int src, int trgt) : this()
        {
            elements[0] = src;
            elements[1] = trgt;
        }


        /**
   * Return the first element of the pair
   */

        public int GetSource()
        {
            return Get(0);
        }

        /**
   * Return the second element of the pair
   */

        public int GetTarget()
        {
            return Get(1);
        }


        //@Override
        public override IntTuple GetCopy()
        {
            return new IntPair(elements[0], elements[1]);
        }

        //@Override
        public override bool Equals(Object iO)
        {
            if (!(iO is IntPair))
            {
                return false;
            }
            var i = (IntPair) iO;
            return elements[0] == i.Get(0) && elements[1] == i.Get(1);
        }

        //@Override
        public override int GetHashCode()
        {
            return elements[0]*17 + elements[1];
        }
    }
}