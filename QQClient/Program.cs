﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using LumaQQ.NET;
using LumaQQ.NET.Utils;
using LumaQQ.NET.Events;
using LumaQQ.NET.Entities;
using LumaQQ.NET.Packets;
using LumaQQ.NET.Packets.In;
using LumaQQ.NET.Packets.Out;
namespace QQClient.cs
{
    class Program
    {
        static void Main(string[] args)
        {
            QQUser user = new QQUser(956625392, "05297crazy");
            LumaQQ.NET.QQClient client = new LumaQQ.NET.QQClient(user);

            #region 代理登录
            //user.IsUdp = false;//如果使用代理必须使用TCP登录
            //client.Proxy.ProxyHost = "192.168.1.100";
            //client.Proxy.ProxyPort = 1100;
            //client.Proxy.ProxyType = ProxyType.Socks5;
            //client.LoginServerHost = "219.133.62.10"; //tcpconn.tencent.com  TCP Server 直接服务器： 219.133.62.10
            //client.LoginPort = 80;
            #endregion

            #region 正常登录
            user.IsUdp = true;
            client.LoginServerHost = "219.133.49.173"; //UDP Server  直接服务器 219.133.62.8，中转服务器 219.133.49.173
            #endregion

            client.PrivateManager.ModifyInfoFailed += new EventHandler<QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket>>(PrivateManager_ModifyInfoFailed);
            client.PrivateManager.ModifyInfoSuccessed += new EventHandler<QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket>>(PrivateManager_ModifyInfoSuccessed);
            client.PrivateManager.ModifySignatureSuccessed += new EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>>(PrivateManager_ModifySignatureSuccessed);
            client.PrivateManager.ModifySignatureFailed += new EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>>(PrivateManager_ModifySignatureFailed);
            client.PrivateManager.SetSearchMeByQQOnlyFailed += new EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>>(PrivateManager_SetSearchMeByQQOnlyFailed);
            client.PrivateManager.SetSearchMeByQQOnlySuccessed += new EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>>(PrivateManager_SetSearchMeByQQOnlySuccessed);
            client.PrivateManager.SetShareGeographyFailed += new EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>>(PrivateManager_SetShareGeographyFailed);
            client.PrivateManager.SetShareGeographySuccessed += new EventHandler<QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket>>(PrivateManager_SetShareGeographySuccessed);
            client.PrivateManager.GetWeatherSuccessed += new EventHandler<QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket>>(PrivateManager_GetWeatherSuccessed);
            client.PrivateManager.GetWeatherFailed += new EventHandler<QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket>>(PrivateManager_GetWeatherFailed);

            client.FriendManager.SignatureChanged += new EventHandler<QQEventArgs<ReceiveIMPacket, OutPacket>>(FriendManager_SignatureChanged);
            client.FriendManager.GetSignatureFailed += new EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>>(FriendManager_GetSignatureFailed);
            client.FriendManager.GetSignatureSuccessed += new EventHandler<QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket>>(FriendManager_GetSignatureSuccessed);
            client.FriendManager.UploadFriendRemarkSuccessed += new EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>>(FriendManager_UploadFriendRemarkSuccessed);
            client.FriendManager.UploadFriendRemarkFailed += new EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>>(FriendManager_UploadFriendRemarkFailed);
            client.FriendManager.DownloadFriendRemarkSuccessed += new EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>>(FriendManager_DownloadFriendRemarkSuccessed);
            client.FriendManager.DownloadFriendRemarkFailed += new EventHandler<QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket>>(FriendManager_DownloadFriendRemarkFailed);
            client.FriendManager.GetFriendLevelSuccessed += new EventHandler<QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket>>(FriendManager_GetFriendLevelSuccessed);
            client.FriendManager.GetUserPropertySuccessed += new EventHandler<QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket>>(FriendManager_GetUserPropertySuccessed);
            client.FriendManager.DownloadGroupFriendSuccessed += new EventHandler<QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket>>(FriendManager_DownloadGroupFriendSuccessed);
            client.FriendManager.DownloadGroupFriendFailed += new EventHandler<QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket>>(FriendManager_DownloadGroupFriendFailed);
            client.FriendManager.DownloadGroupNamesFailed += new EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>>(FriendManager_DownloadGroupNamesFailed);
            client.FriendManager.DownloadGroupNamesSuccessed += new EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>>(FriendManager_DownloadGroupNamesSuccessed);
            client.FriendManager.UploadGroupNamesSuccessed += new EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>>(FriendManager_UploadGroupNamesSuccessed);
            client.FriendManager.UploadGroupNamesFailed += new EventHandler<QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket>>(FriendManager_UploadGroupNamesFailed);
            client.FriendManager.SearchUserSuccessed += new EventHandler<QQEventArgs<SearchUserReplyPacket, SearchUserPacket>>(FriendManager_SearchUserSuccessed);

            client.Error += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.ErrorPacket, OutPacket>>(client_Error);
            client.ConnectionManager.NetworkError += new EventHandler<LumaQQ.NET.Events.ErrorEventArgs>(ConnectionManager_NetworkError);
            client.ConnectionManager.ConnectServerError += new EventHandler<LumaQQ.NET.Events.ErrorEventArgs>(ConnectionManager_ConnectServerError);
            client.ConnectionManager.ReceivedKeepAlive += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.KeepAliveReplyPacket, KeepAlivePacket>>(ConnectionManager_ReceivedKeepAlive);
            client.ConnectionManager.ConnectSuccessed += new EventHandler(ConnectionManager_ConnectSuccessed);

            client.PacketManager.ReceivedUnknownPacket += new EventHandler<QQEventArgs<UnknownInPacket, OutPacket>>(PacketManager_ReceivedUnknownPacket);
            client.PacketManager.SendPacketSuccessed += new EventHandler<QQEventArgs<InPacket, OutPacket>>(PacketManager_SendPacketSuccessed);
            client.PacketManager.SendPacketTimeOut += new EventHandler<QQEventArgs<InPacket, OutPacket>>(PacketManager_SendPacketTimeOut);
            client.PacketManager.LostConnection += new EventHandler<QQEventArgs<InPacket, OutPacket>>(PacketManager_LostConnection);
            client.MessageManager.ReceiveNormalIM += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.ReceiveIMPacket, OutPacket>>(MessageManager_ReceiveNormalIM);
            client.MessageManager.ReceiveDuplicatedIM += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.ReceiveIMPacket, OutPacket>>(MessageManager_ReceiveDuplicatedIM);
            client.MessageManager.SysAddedByOthers += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysAddedByOthers);
            client.MessageManager.SysAdvertisment += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysAdvertisment);
            client.MessageManager.SysAddOtherApproved += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysAddOtherApproved);
            client.MessageManager.SysAddedByOthersEx += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysAddedByOthersEx);
            client.MessageManager.SysApprovedAddOtherAndAddMe += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysApprovedAddOtherAndAddMe);
            client.MessageManager.SysAddOtherRejected += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysAddOtherRejected);
            client.MessageManager.SysRequestAddMe += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysRequestAddMe);
            client.MessageManager.SysRequestAddMeEx += new EventHandler<QQEventArgs<SystemNotificationPacket, OutPacket>>(MessageManager_SysRequestAddMeEx);

            client.FriendManager.GetFriendListSuccessed += new EventHandler<QQEventArgs<GetFriendListReplyPacket, GetFriendListPacket>>(FriendManager_GetFriendListSuccessed);
            client.FriendManager.GetOnlineFriendSuccessed += new EventHandler<QQEventArgs<GetOnlineOpReplyPacket, GetOnlineOpPacket>>(FriendManager_GetOnlineFriendSuccessed);
            client.FriendManager.GetUserInfoSuccessed += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.GetUserInfoReplyPacket, GetUserInfoPacket>>(FriendManager_GetUserInfoSuccessed);
            client.FriendManager.FriendChangeStatus += new EventHandler<QQEventArgs<FriendChangeStatusPacket, OutPacket>>(FriendManager_FriendChangeStatus);
            client.FriendManager.ChangeStatusSuccessed += new EventHandler<QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket>>(FriendManager_ChangeStatusSuccessed);
            client.FriendManager.ChangeStatusFailed += new EventHandler<QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket>>(FriendManager_ChangeStatusFailed);
            client.FriendManager.AddFriendSuccessed += new EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>>(FriendManager_AddFriendSuccessed);
            client.FriendManager.AddFriendNeedAuth += new EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>>(FriendManager_AddFriendNeedAuth);
            client.FriendManager.AddFriendDeny += new EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>>(FriendManager_AddFriendDeny);
            client.FriendManager.AddFriendFailed += new EventHandler<QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket>>(FriendManager_AddFriendFailed);
            client.FriendManager.DeleteFriendSuccessed += new EventHandler<QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket>>(FriendManager_DeleteFriendSuccessed);
            client.FriendManager.DeleteFriendFailed += new EventHandler<QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket>>(FriendManager_DeleteFriendFailed);
            client.FriendManager.SendAuthSuccessed += new EventHandler<QQEventArgs<AuthorizeReplyPacket, AuthorizePacket>>(FriendManager_SendAuthSuccessed);
            client.FriendManager.SendAuthFailed += new EventHandler<QQEventArgs<AuthorizeReplyPacket, AuthorizePacket>>(FriendManager_SendAuthFailed);
            client.FriendManager.ResponseAuthSuccessed += new EventHandler<QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket>>(FriendManager_ResponseAuthSuccessed);
            client.FriendManager.ResponseAuthFailed += new EventHandler<QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket>>(FriendManager_ResponseAuthFailed);
            client.FriendManager.UploadGroupFriendFailed += new EventHandler<QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket>>(FriendManager_UploadGroupFriendFailed);
            client.FriendManager.UploadGroupFriendSuccessed += new EventHandler<QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket>>(FriendManager_UploadGroupFriendSuccessed);
            #region login events
            client.LoginManager.LoginSuccessed += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket>>(LoginManager_LoginSuccessed);
            client.LoginManager.LoginFailed += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket>>(LoginManager_LoginFailed);
            client.LoginManager.LoginRedirect += new EventHandler<LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket>>(LoginManager_LoginRedirect);
            #endregion



            client.Login();
            //Console.WriteLine(LumaQQ.NET.Utils.Util.GetTimeMillis(DateTime.Now));
            Console.WriteLine("回车退出登录:");
            Console.ReadLine();
            client.LoginManager.Logout();
            WL("QQ退出成功！");
        }

        static void PrivateManager_GetWeatherFailed(object sender, QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket> e)
        {
            WL("读取天气预报失败");
        }

        static void PrivateManager_GetWeatherSuccessed(object sender, QQEventArgs<WeatherOpReplyPacket, WeatherOpPacket> e)
        {
            WL("成功读取天气预报，地区：{0} {1} 日期：{4} 最高温度:{2} 最低温度:{3}", e.InPacket.Province, e.InPacket.City, e.InPacket.Weathers[0].HighTemperature, e.InPacket.Weathers[0].LowTemperature, Util.GetDateTimeFromMillis(e.InPacket.Weathers[0].Day));
        }

        static void FriendManager_SearchUserSuccessed(object sender, QQEventArgs<SearchUserReplyPacket, SearchUserPacket> e)
        {
            if (e.InPacket.Finished)
            {
                WL("已成功返回所有找到的用户");
            }
            else
            {
                WL("此次共找到{0}位好友", e.InPacket.Users.Count);
            }

        }

        static void FriendManager_UploadGroupNamesFailed(object sender, QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            WL("上传分组失败");
        }

        static void FriendManager_UploadGroupNamesSuccessed(object sender, QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            WL("上传分组成功，重新读取分组");
            e.QQClient.FriendManager.DownloadGroupName();
        }

        static void FriendManager_DownloadGroupNamesSuccessed(object sender, QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            WL("成功下载分组名称，共有{0}个分组", e.InPacket.GroupNames.Count);
            foreach (string s in e.InPacket.GroupNames)
            {
                WL("组名称：{0}", s);
            }
        }

        static void FriendManager_DownloadGroupNamesFailed(object sender, QQEventArgs<GroupDataOpReplyPacket, GroupDataOpPacket> e)
        {
            WL("下载分组名称失败");
        }

        static void FriendManager_DownloadGroupFriendFailed(object sender, QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket> e)
        {
            WL("下载分组好失败");
        }

        static void FriendManager_DownloadGroupFriendSuccessed(object sender, QQEventArgs<DownloadGroupFriendReplyPacket, DownloadGroupFriendPacket> e)
        {
            foreach (DownloadFriendEntry friend in e.InPacket.Friends)
            {
                WL("QQ:{0} 在第 {1} 组，类型：{2}", friend.QQ, friend.Group, friend.Type);
            }
        }

        static void FriendManager_GetUserPropertySuccessed(object sender, QQEventArgs<UserPropertyOpReplyPacket, UserPropertyOpPacket> e)
        {
            if (e.InPacket.Finished)
            {
                WL("已经读取全部好友的属性信息");
            }
            foreach (UserProperty property in e.InPacket.Properties)
            {
                WL("用户属性： QQ:{0} 属性值：{1}", property.QQ, property.Property);
            }
        }

        static void FriendManager_GetFriendLevelSuccessed(object sender, QQEventArgs<FriendLevelOpReplyPacket, FriendLevelOpPacket> e)
        {
            WL("成功查询好友等级");
            foreach (FriendLevel level in e.InPacket.FriendLevels)
            {
                WL("QQ：{0} 等级：{1} 活动天数:{2} 升级天数：{3}", level.QQ, level.Level, level.ActiveDays, level.UpgradeDays);
            }
        }

        static void FriendManager_DownloadFriendRemarkFailed(object sender, QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            WL("下载好友备注信息失败");
        }

        static void FriendManager_DownloadFriendRemarkSuccessed(object sender, QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            if (e.InPacket.Remark != null)
            {
                WL("下载好友备注信息成功，QQ：{0}，好友备注名称为：{1}", e.InPacket.QQ, e.InPacket.Remark.Name);
            }
            else
            {
                WL("下载好友备注信息成功，但是却无法得好友备注资料");
            }
        }

        static void FriendManager_UploadFriendRemarkFailed(object sender, QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            WL("成功修改好友备注信息失败");
        }

        static void FriendManager_UploadFriendRemarkSuccessed(object sender, QQEventArgs<FriendDataOpReplyPacket, FriendDataOpPacket> e)
        {
            WL("成功修改好友备注信息");
            e.QQClient.FriendManager.DownloadFriendRemark(e.InPacket.QQ);
        }

        static void FriendManager_SignatureChanged(object sender, QQEventArgs<ReceiveIMPacket, OutPacket> e)
        {
            WL("好友：{0},个性签名改为：{1}", e.InPacket.SignatureOwner, e.InPacket.Signature);
        }

        static void PrivateManager_SetShareGeographySuccessed(object sender, QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            WL("成功设置共享地理位置选项");
        }

        static void PrivateManager_SetShareGeographyFailed(object sender, QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            WL("设置共享地理位置失败");
        }

        static void PrivateManager_SetSearchMeByQQOnlySuccessed(object sender, QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            WL("成功设置只能通过QQ找到我");
        }

        static void PrivateManager_SetSearchMeByQQOnlyFailed(object sender, QQEventArgs<PrivacyDataOpReplyPacket, PrivacyDataOpPacket> e)
        {
            WL("设置只能通过QQ找到我失败");
        }

        static void FriendManager_GetSignatureSuccessed(object sender, QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            WL("成功读取个性签名，QQ：{0} 签名：{1} 修改时间：{2}", e.InPacket.Signatures[0].QQ, e.InPacket.Signatures[0].Sig, e.InPacket.Signatures[0].ModifiedTime);
        }

        static void FriendManager_GetSignatureFailed(object sender, QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            WL("读取个性签名失败");
        }

        static void PrivateManager_ModifySignatureFailed(object sender, QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            WL("修改个性签名失败");
        }

        static void PrivateManager_ModifySignatureSuccessed(object sender, QQEventArgs<SignatureOpReplyPacket, SignatureOpPacket> e)
        {
            WL("成功修改个性签名,重新读取个性签名");
            e.QQClient.FriendManager.GetSignature(e.QQClient.QQUser.QQ);
        }

        static void PrivateManager_ModifyInfoFailed(object sender, QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket> e)
        {
            if (e.OutPacket.ModifyPassword)
            {
                WL("修改密码失败,Success{0}", e.InPacket.Success);
            }
            else
                WL("修改个人信息失败");
        }

        static void PrivateManager_ModifyInfoSuccessed(object sender, QQEventArgs<ModifyInfoReplyPacket, ModifyInfoPacket> e)
        {
            if (e.OutPacket.ModifyPassword)
            {
                WL("修改密码成功，请使用新密码登录");
            }
            else
            {
                WL("修改个人信息成功，重新读取个人信息");
                e.QQClient.FriendManager.GetUserInfo(e.InPacket.QQ);
            }
        }

        static void PacketManager_LostConnection(object sender, QQEventArgs<InPacket, OutPacket> e)
        {
            WL("连接丢失，无法发送包：{0}", e.OutPacket);
        }

        static void PacketManager_SendPacketTimeOut(object sender, QQEventArgs<InPacket, OutPacket> e)
        {
            WL("包发送超时{0}", e.OutPacket);
        }

        static void PacketManager_SendPacketSuccessed(object sender, QQEventArgs<InPacket, OutPacket> e)
        {
            WL("包发送成功:{0}", e.OutPacket);
        }

        static void PacketManager_ReceivedUnknownPacket(object sender, QQEventArgs<UnknownInPacket, OutPacket> e)
        {
            WL("接收到未知包：{0}", e);
        }

        static void FriendManager_UploadGroupFriendSuccessed(object sender, QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket> e)
        {
            WL("上传好友分组成功");
        }

        static void FriendManager_UploadGroupFriendFailed(object sender, QQEventArgs<UploadGroupFriendReplyPacket, UploadGroupFriendPacket> e)
        {
            WL("上传好友分组失败");
        }

        static void FriendManager_DeleteFriendFailed(object sender, QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket> e)
        {
            WL("删除好友{0}失败", e.OutPacket.To);
        }

        static void FriendManager_DeleteFriendSuccessed(object sender, QQEventArgs<DeleteFriendReplyPacket, DeleteFriendPacket> e)
        {
            WL("删除好友{0}成功", e.OutPacket.To);
        }

        static void FriendManager_ResponseAuthFailed(object sender, QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket> e)
        {
            WL("处理({0})认证信息失败", e.OutPacket.Action);
        }

        static void FriendManager_ResponseAuthSuccessed(object sender, QQEventArgs<AddFriendAuthResponseReplyPacket, AddFriendAuthResponsePacket> e)
        {
            WL("处理{0}认证信息成功", e.OutPacket.Action);
        }

        static void FriendManager_SendAuthFailed(object sender, QQEventArgs<AuthorizeReplyPacket, AuthorizePacket> e)
        {
            WL("发送身份认证({0})失败", e.OutPacket.Message);
        }

        static void FriendManager_SendAuthSuccessed(object sender, QQEventArgs<AuthorizeReplyPacket, AuthorizePacket> e)
        {
            WL("发送身份认证({0})成功", e.OutPacket.Message);
        }

        static void FriendManager_AddFriendFailed(object sender, QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            WL("添加好友{0}失败", e.InPacket.FriendQQ);
        }

        static void FriendManager_AddFriendDeny(object sender, QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            WL("添加好友{0}被拒绝", e.InPacket.FriendQQ);
        }

        static void FriendManager_AddFriendNeedAuth(object sender, QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            WL("添加好友{0}需要身份验证，自动发送验证信息", e.InPacket.FriendQQ);
            e.QQClient.FriendManager.SendAddFriendAuth(e.InPacket.FriendQQ, "我是你的好朋友啊！");
        }

        static void FriendManager_AddFriendSuccessed(object sender, QQEventArgs<AddFriendExReplyPacket, AddFriendExPacket> e)
        {
            WL("成功添加{0}为好友", e.InPacket.FriendQQ);
        }

        static void MessageManager_SysRequestAddMeEx(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("{0}使用0x00A8命令请求加我为好友,附加信息:{1}", e.InPacket.From, e.InPacket.Message);
            e.QQClient.FriendManager.ApprovedAddMe(e.InPacket.From);
            e.QQClient.FriendManager.AddFriend(e.InPacket.From);
            //e.QQClient.FriendManager.SendAddFriendAuth(e.InPacket.From, "我是你的好朋友啊！");
            // e.QQClient.FriendManager.AddFriendToList(0, e.InPacket.From);
        }

        static void MessageManager_SysRequestAddMe(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("{0}请求加我为好友,附加信息:{1}", e.InPacket.From, e.InPacket.Message);
            e.QQClient.FriendManager.AddFriend(e.InPacket.From);
        }

        static void MessageManager_SysAddOtherRejected(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("{0}拒绝了的添加请求，理由：{1}", e.InPacket.From, e.InPacket.Message);
        }

        static void MessageManager_SysApprovedAddOtherAndAddMe(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("我已同意{0}加我为好友，并将对方加为好友。", e.InPacket.From);
        }

        static void MessageManager_SysAddedByOthersEx(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("{0}使用使用0x00A8命令把我加为好友", e.InPacket.From);
        }

        static void MessageManager_SysAddOtherApproved(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("我已同意{0}加我为好友。", e.InPacket.From);
        }

        static void MessageManager_SysAdvertisment(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("收到广告信息：{0}", e.InPacket.Message);
        }

        static void MessageManager_SysAddedByOthers(object sender, QQEventArgs<SystemNotificationPacket, OutPacket> e)
        {
            WL("被{0}加为好友", e.InPacket.From);
        }

        static void FriendManager_ChangeStatusFailed(object sender, QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket> e)
        {
            WL("状态改变失败!");
        }

        static void FriendManager_ChangeStatusSuccessed(object sender, QQEventArgs<ChangeStatusReplyPacket, ChangeStatusPacket> e)
        {
            WL("状态改变成功，新状态是：{0}", e.QQClient.QQUser.Status);
        }

        static void FriendManager_FriendChangeStatus(object sender, QQEventArgs<FriendChangeStatusPacket, OutPacket> e)
        {
            WL("好友:{0} 状态改变为：{1}", e.InPacket.FriendQQ, e.InPacket.Status);
        }

        static void ConnectionManager_ConnectSuccessed(object sender, EventArgs e)
        {
            WL("服务器连接成功！");
        }

        static void FriendManager_GetUserInfoSuccessed(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.GetUserInfoReplyPacket, GetUserInfoPacket> e)
        {
            WL("得到{0}的详细资料它的昵称是：{1}，城市：{2}", e.InPacket.ContactInfo.QQ, e.InPacket.ContactInfo.Nick, e.InPacket.ContactInfo.City);
            //修改密码
            e.QQClient.PrivateManager.ModifyPassword("qqtest123", "qqtest1234");
        }

        static void FriendManager_GetOnlineFriendSuccessed(object sender, QQEventArgs<GetOnlineOpReplyPacket, GetOnlineOpPacket> e)
        {
            if (e.InPacket.Finished)
            {
                WL("共得到{0}位在线好友", e.QQClient.QQUser.Friends.Onlines);
                //  e.QQClient.FriendManager.AddFriend(630377892);
            }
            else
            {
                WL("本次共得到{0}位在线好友", e.InPacket.OnlineFriends.Count);
            }
        }

        static void FriendManager_GetFriendListSuccessed(object sender, QQEventArgs<GetFriendListReplyPacket, GetFriendListPacket> e)
        {
            if (e.InPacket.Finished)
            {
                WL("共得到{0}位好友", e.QQClient.QQUser.Friends.Count);
                WL("开始读取在线好友");
                e.QQClient.FriendManager.GetOnlineFriend();
                int i = 0;
                List<int> list = new List<int>();
                foreach (FriendInfo info in e.QQClient.QQUser.Friends.Values)
                {
                    WL("第" + (i++).ToString() + "位好友：" + info.BasicInfo.Nick + "(" + info.BasicInfo.QQ.ToString() + ")");

                    //修改好友备注
                    FriendRemark remark = new FriendRemark();
                    remark.Name = "好友" + i.ToString();
                    e.QQClient.FriendManager.UploadFriendRemark(info.BasicInfo.QQ, remark);

                    list.Add(info.BasicInfo.QQ);
                }
                //查询好友等级
                e.QQClient.FriendManager.GetFriendLevel(list);

                //读取好友属性信息
                e.QQClient.FriendManager.GetUserProperty(0);
            }
            else
            {
                WL("本次共得到{0}位好友", e.InPacket.Friends.Count);
            }
        }

        static void MessageManager_ReceiveDuplicatedIM(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.ReceiveIMPacket, OutPacket> e)
        {
            if (e.InPacket.NormalIM != null)
            {
                WL("重复收到好友：{0} 发来的信息：{1}", e.InPacket.NormalHeader.Sender, e.InPacket.NormalIM.Message);
            }
            else
            {
                WL("重复收到包:{0}", e.InPacket);
            }
        }

        static void ConnectionManager_ConnectServerError(object sender, LumaQQ.NET.Events.ErrorEventArgs e)
        {
            WL("连接服务器失败：{0}", e.Exception.Message);
        }

        static void MessageManager_ReceiveNormalIM(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.ReceiveIMPacket, OutPacket> e)
        {
            WL("收到好友：{0} 发来的信息：{1}", e.InPacket.NormalHeader.Sender, e.InPacket.NormalIM.Message);
            e.QQClient.MessageManager.SendIM(e.InPacket.NormalHeader.Sender, string.Format("我收到你的消息：{0}", e.InPacket.NormalIM.Message));
        }

        static void ConnectionManager_ReceivedKeepAlive(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.KeepAliveReplyPacket, KeepAlivePacket> e)
        {
            WL("KeepAlive Packet ，IP：{0}在线人数：{1}", e.InPacket.IP, e.InPacket.Onlines.ToString());
        }

        static void LoginManager_LoginRedirect(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket> e)
        {
            WL("重定向登录，服务器IP：" + e.InPacket.RedirectIPString);
        }

        static void LoginManager_LoginFailed(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket> e)
        {
            WL("登录失败，原因：" + e.InPacket.ReplyMessage);
        }

        static void LoginManager_LoginSuccessed(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.In.LoginReplyPacket, LoginPacket> e)
        {
            Console.WriteLine("登录成功");
            WL("开始读取好友信息");
            e.QQClient.FriendManager.GetFriendList();
            e.QQClient.FriendManager.GetUserInfo(e.QQClient.QQUser.QQ);
            //改变状态为离开状态
            e.QQClient.FriendManager.ChangeStatus(QQStatus.AWAY, false);
            //修改个人信息
            ContactInfo contact = new ContactInfo();
            contact.Nick = "LumaQQ.NET";
            contact.AuthType = AuthType.No;
            contact.City = "福州@" + DateTime.Now.ToString();
            e.QQClient.PrivateManager.ModifyInfo(contact);
            //修改个性签名
            e.QQClient.PrivateManager.ModifySignature("LumaQQ.NET 个性签名 @" + DateTime.Now.ToString());
            //设置只能通过号码找到我
            e.QQClient.PrivateManager.SetSearchMeByQQOnly(true);
            //设置为不共享地理位置 
            e.QQClient.PrivateManager.ShareGeography(false);

            //下载分组好友
            e.QQClient.FriendManager.DownloadGroupFriends(0);
            //上传分组，成功后下载分组
            List<string> groups = new List<string>();
            groups.Add(DateTime.Now.ToString());
            e.QQClient.FriendManager.UploadGroupName(groups);

            //搜索好友
            e.QQClient.FriendManager.SearchUser(0);

            //读天气预报
            e.QQClient.PrivateManager.GetWeather();
        }

        static void client_Error(object sender, LumaQQ.NET.Events.QQEventArgs<LumaQQ.NET.Packets.ErrorPacket, OutPacket> e)
        {
            Console.WriteLine("出错啦：{0}", e.InPacket.ErrorMessage);
            //e.QQClient.LoginManager.Logout();
        }

        static void ConnectionManager_NetworkError(object sender, LumaQQ.NET.Events.ErrorEventArgs e)
        {
            Console.WriteLine(e.Exception.Message);
            Console.WriteLine(e.Exception.StackTrace);
        }

        #region Helper methods

        private static void WL(object text, params object[] args)
        {
            Console.WriteLine(text.ToString(), args);
        }

        private static void RL()
        {
            Console.ReadLine();
        }

        private static void Break()
        {
            System.Diagnostics.Debugger.Break();
        }

        #endregion
    }
}
