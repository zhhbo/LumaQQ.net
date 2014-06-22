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

namespace LumaQQ.NET.Packets.Out
{
    /// <summary> * 激活临时群：
    /// * 1. 头部
    /// * 2. 群命令类型，1字节，0x37
    /// * 3. 临时群类型，1字节，0x01是多人对话，0x02是讨论组
    /// * 4. 父群内部ID，4字节
    /// * 5. 临时群内部ID，4字节
    /// * 6. 尾部
    /// 	<remark>abu 2008-02-28 </remark>
    /// </summary>
    public class ClusterActivateTempPacket : ClusterCommandPacket
    {
        public byte Type { get; set; }
        public int ParentClusterId { get; set; }
        public ClusterActivateTempPacket(QQUser user) : base(user)
        {
            SubCommand = ClusterCommand.ACTIVATE_TEMP;
        }
        public ClusterActivateTempPacket(ByteBuffer buf, int length, QQUser user) : base(buf, length, user) { }
        public override string GetPacketName()
        {
            return "Cluster Get Temp Cluster Info Packet";
        }
        protected override void PutBody(ByteBuffer buf)
        {
            // 命令类型
            buf.Put((byte)SubCommand);
            // 类型
            buf.Put(Type);
            // 内部ID
            buf.PutInt(ParentClusterId);
            // 外部ID
            buf.PutInt(ClusterId);
        }
    }
}
