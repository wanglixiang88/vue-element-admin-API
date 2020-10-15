﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIExample.Engine
{
    public class CommonResult<T>
    {

        /// <summary>
        /// 是否成功
        /// </summary>
        public bool bSucceed { get; set; }

        public int errCode { get; set; }

        public String errMsg { get; set; }

        public T result { get; set; }
    }
}