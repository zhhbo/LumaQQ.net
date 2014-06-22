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
using System.Net;
using System.Net.Sockets;

using Org.Mentalis.Network.ProxySocket;

namespace LumaQQ.NET.Net.Sockets
{
    public class UDPConnection : SocketConnection
    {
        public UDPConnection(ConnectionPolicy policy, EndPoint server)
            : base(policy, server)
        {

        }
        protected override Org.Mentalis.Network.ProxySocket.ProxySocket GetSocket()
        {
            if (socket == null)
            {
                socket = new ProxySocket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                socket.ProxyType = ProxyTypes.None;
                //socket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.SendTimeout, 3000);
            }
            return socket;
        }

        /// <summary>
        /**
        * 添加代理包的头部，Socks5代理包的格式为
        * +----+------+------+----------+----------+----------+
        * |RSV | FRAG | ATYP | DST.ADDR | DST.PORT |   DATA   |
        * +----+------+------+----------+----------+----------+
        * | 2  |  1   |  1   | Variable |    2     | Variable |
        * +----+------+------+----------+----------+----------+
        */
        /// 	<remark>abu 2008-03-05 </remark>
        /// </summary>
        /// <param name="buf">The buf.</param>
        protected override void FillHeader(ByteBuffer buf)
        {
            //  buf.PutChar((char)0);
            //buf.Put((byte)0)
            //buf.Put(0x1);
            //buf.Put(this.)
            //buf.putChar((char)proxy.remotePort);
        }
    }
}
