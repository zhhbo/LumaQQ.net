﻿#region 版权声明
/**
 * 版权声明：LumaQQ.NET是基于LumaQQ分析的QQ协议，将其部分代码进行修改和翻译为.NET版本，并且继续使用LumaQQ的开源协议。
 * 本人没有对其核心协议进行改动， 也没有与腾讯公司的QQ软件有直接联系，请尊重LumaQQ作者Luma的著作权和版权声明。
 * 同时在使用此开发包前请自行协调好多方面关系，本人不享受和承担由此产生的任何权利以及任何法律责任。
 * 
 * 作者：阿不
 * 博客：http://hjf1223.cnblogs.com
 * Email：hjf1223@gmail.com
 * LumaQQ：http://lumaqq.linuxsir.org 
 * LumaQQ - Java QQ Client
 * 
 * Copyright (C) 2004 luma <stubma@163.com>
 * 
 * LumaQQ - For .NET QQClient
 * Copyright (C) 2008 阿不<hjf1223@gmail.com>
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 2 of the License, or
 * (at your option) any later version.
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 * GNU General Public License for more details.
 * 
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace LumaQQ.NET.Packets.In
{
    /// <summary>
    ///  * 临时会话操作回复包，格式为
    /// * 1. 头部
    /// * 2. 子命令，1字节
    /// * 当2部分为0x01时，格式为
    /// * 3. 接收者QQ号，4字节
    /// * 4. 回复码，1字节
    /// * 5. 回复消息长度，1字节
    /// * 6. 回复消息
    /// * 7. 尾部
    /// 	<remark>abu 2008-02-26 </remark>
    /// </summary>
    public class TempSessionOpReplyPacket : BasicInPacket
    {
        public string ReplyMessage { get; set; }
        public int Receiver { get; set; }
        public ReplyCode ReplyCode { get; set; }
        public TempSessionSubCmd SubCommand { get; set; }
        public TempSessionOpReplyPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            switch (SubCommand)
            {
                case TempSessionSubCmd.SendIM:
                    return "Temp Session IM Reply Packet";
                default:
                    return "Unknown Temp Session Op Reply Packet";                    
            }
        }
        protected override void ParseBody(ByteBuffer buf)
        {
            SubCommand = (TempSessionSubCmd)buf.Get();
            switch (SubCommand)
            {
                case TempSessionSubCmd.SendIM:
                    Receiver = buf.GetInt();
                    ReplyCode = (ReplyCode)buf.Get();
                    int len = buf.Get() & 0xFF;
                    ReplyMessage = Utils.Util.GetString(buf, len);
                    break;
                default:
                    break;
            }
        }
    }
}
