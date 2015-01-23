using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Weixin
{
    public static class WxServiceHelper
    {
        private static AccessToken WxToken;

        public static AccessToken Token
        {
            get
            {
                if (WxToken == null ||WxToken.access_token==""||
                    !WxToken.IsExpired)
                {
                    WxToken = new AccessToken().Renew();
                };
                return WxToken;
            }
        }


    }
}
