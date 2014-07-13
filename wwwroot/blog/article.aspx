<%@ Page Title="" Language="C#" MasterPageFile="BlogMasterPage.master" ValidateRequest="false"
    AutoEventWireup="true" Inherits="Home.blog_article" %>

<asp:Content ID="Content1" ContentPlaceHolderID="BlogContent" runat="Server">
    <link rel="stylesheet" type="text/css" href="SHJS/css/sh_the.css" />

    <script src="SHJS/sh_main.js" type="text/javascript"></script>

    <script src="SHJS/lang/sh_javascript.js" type="text/javascript"></script>

    <script src="SHJS/lang/sh_cpp.js" type="text/javascript"></script>

    <script src="SHJS/lang/sh_sql.js" type="text/javascript"></script>

    <script src="SHJS/lang/sh_csharp.js" type="text/javascript"></script>

    <script language="javascript" type="text/javascript">
        function on_submit_comment()
        {
            if (document.getElementById("comment_name").value == "")
            {
                alert("请输入昵称！");
                document.getElementById("comment_name").focus();
                return false;
            }
            if (document.getElementById("comment_message").value == "")
            {
                alert("请输入评论内容！");
                document.getElementById("comment_message").focus();
                return false;
            }
            if (document.getElementById("comment_verifycode").value == "")
            {
                alert("请输入验证码！");
                document.getElementById("comment_verifycode").focus();
                return false;
            }
            var state = GetState();
            if (Core.GetBrowser() == "IE") state["ScrollTop"] = document.documentElement.scrollTop;
            else state["ScrollTop"] = document.body.scrollTop;
            ResetState(state);

            return true;
        }
        function on_submit_reply_comment()
        {
            if (document.getElementById("reply_name").value == "")
            {
                alert("请输入昵称！");
                document.getElementById("reply_name").focus();
                return false;
            }
            if (document.getElementById("reply_message").value == "")
            {
                alert("请输入评论内容！");
                document.getElementById("reply_message").focus();
                return false;
            }
            if (document.getElementById("reply_verifycode").value == "")
            {
                alert("请输入验证码！");
                document.getElementById("reply_verifycode").focus();
                return false;
            }
            var state = GetState();
            if (Core.GetBrowser() == "IE") state["ScrollTop"] = document.documentElement.scrollTop;
            else state["ScrollTop"] = document.body.scrollTop;
            ResetState(state);

            return true;
        }

        function DeleteComment(id, content)
        {
            if (confirm(String.format("您确定要删除评论\"{0}\"？", content)))
            {
                var state = GetState();
                if (Core.GetBrowser() == "IE") state["ScrollTop"] = document.documentElement.scrollTop;
                else state["ScrollTop"] = document.body.scrollTop;
                ResetState(state);

                DoCommand("DeleteComment", id);
            }
        }

        function ReplyTo(id)
        {
            var rf = document.getElementById("replyform");
            rf.parentNode.removeChild(rf);
            rf.style.display = "block";
            document.getElementById("replyform_container_" + id).appendChild(rf);

            var state = GetState();
            state["ReplyTo"] = id;
            ResetState(state);

        }

        function RefreshVerifyCode(force)
        {
            if (force == undefined) force = false;
            if (!force && document.getElementById("verifycode_guid").value != "") return;
            document.getElementById("comment_verifycode_image").style.display = "inline";
            document.getElementById("reply_verifycode_image").style.display = "inline";

            try
            {
                var request = null;
                if (window.XMLHttpRequest)
                {
                    request = new XMLHttpRequest();
                }
                else if (window.ActiveXObject)
                {
                    request = new ActiveXObject("Microsoft.XMLHttp");
                }

                request.onreadystatechange = function()
                {
                    if (request.readyState == 4)
                    {
                        switch (request.status)
                        {
                            case 200:
                                {
                                    var ret = eval(request.responseText);
                                    var img_src = "../VerifyCode.ashx?file=" + ret.Guid + ".png&" + (new Date() - new Date(2009, 0, 1));
                                    document.getElementById("comment_verifycode_image").src = img_src;
                                    document.getElementById("reply_verifycode_image").src = img_src;
                                    document.getElementById("verifycode_guid").value = ret.Guid;
                                    break;
                                }
                        }
                    }
                }

                request.open("POST", "../VerifyCode.ashx", true);
                request.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                request.send("Guid=" + document.getElementById("verifycode_guid").value);
            }
            catch (ex)
            {
            }
        }
    </script>

    <div class="post no-bg">
        <h2>
            <a href="article.aspx?ID=<%= ArticleID %>">
                <%= ArticleTitle%></a></h2>
        <p class="post-info">
            作者：<a href="javascript:void(0)">卢春城</a></p>
        <%= Content %>
        <p class="postmeta">
            <a class="comments" href="#a_submit_comment">评论 (<%=CommentCount%>)</a> | <span class="date">
                更新于：<%= RenewTime %></span>
            <%-- |<a href="edit.aspx?ID=<%= ArticleID %>" class="edit">编辑</a> --%>
        </p>
    </div>
    <%= RenderCommentHtml(0, 0) %>
    <a name='a_submit_comment'></a>
    <h3 id="respond">
        发表评论</h3>
    <div id="commentform">
        <p>
            <label for="name">
                名字(必须)：</label><br />
            <input <%= LUCC.Global.CurrentUser == null ? "" : "readonly='readonly'" %> class="text"
                id="comment_name" name="comment_name" value="<%= LUCC.Global.CurrentUser == null ? "" : LUCC.Global.CurrentUser.Nickname %>"
                type="text" tabindex="6" />
        </p>
        <p>
            <label for="email">
                电子邮件：</label><br />
            <input <%= LUCC.Global.CurrentUser == null ? "" : "readonly='readonly'" %> class="text"
                id="comment_email" name="comment_email" value="<%= LUCC.Global.CurrentUser == null ? "" : LUCC.Global.CurrentUser.EMail %>"
                type="text" tabindex="7" />
        </p>
        <p>
            <label for="message">
                评论内容(必须)：</label><br />
            <textarea id="comment_message" name="comment_message" rows="10" cols="20" tabindex="8"></textarea>
        </p>
        <p>
            <label for="message">
                验证码(必须)：</label>
            <input id="verifycode_guid" name="verifycode_guid" type="hidden" />
            <input tabindex="7" id="comment_verifycode" name="comment_verifycode" type="text"
                style="width: 80px;" onfocus="return RefreshVerifyCode()"  tabindex="9"/>
            <img id="comment_verifycode_image" title="点击刷新验证码" style="width: 76px; height: 26px;
                margin: 0px; padding: 0px; cursor: pointer; display: none;" onclick="return RefreshVerifyCode(true)" />
        </p>
        <p class="no-border">
            <input class="button" type="submit" name="submit_comment" value="提 交" tabindex="10"
                onclick="return on_submit_comment()" />
        </p>
    </div>
    <div id="replyform" style="display: none; background-color: White;">
        <p>
            <label for="name">
                名字(必须)：</label><br />
            <input <%= LUCC.Global.CurrentUser == null ? "" : "readonly='readonly'" %> class="text"
                id="reply_name" name="reply_name" value="<%= LUCC.Global.CurrentUser == null ? "" : LUCC.Global.CurrentUser.Nickname %>"
                type="text" tabindex="1" style="width: 90%;" />
        </p>
        <p>
            <label for="email">
                电子邮件：</label><br />
            <input <%= LUCC.Global.CurrentUser == null ? "" : "readonly='readonly'" %> class="text"
                id="reply_email" name="reply_email" value="<%= LUCC.Global.CurrentUser == null ? "" : LUCC.Global.CurrentUser.EMail %>"
                type="text" tabindex="2" style="width: 90%;" />
        </p>
        <p>
            <label for="message">
                评论内容(必须)：</label><br />
            <textarea id="reply_message" name="reply_message" rows="5" tabindex="3" style="width: 90%;"></textarea>
        </p>
        <p>
            <label for="message">
                验证码(必须)：</label>
            <input tabindex="4" id="reply_verifycode" name="reply_verifycode" type="text" style="width: 80px;"
                onfocus="return RefreshVerifyCode()" />
            <img id="reply_verifycode_image" title="点击刷新验证码" style="width: 76px; height: 26px;
                margin: 0px; padding: 0px; cursor: pointer; display: none;" onclick="return RefreshVerifyCode(true)" />
        </p>
        <p class="no-border">
            <input class="button" type="submit" name="submit_reply_comment" value="提 交" tabindex="5"
                onclick="return on_submit_reply_comment()" />
        </p>
    </div>

    <script language="javascript" type="text/javascript">

        if (window.attachEvent)
        {
            window.attachEvent("onload", function() { sh_highlightDocument(); document.getElementById("verifycode_guid").value = ""; });
        }
        else if (window.addEventListener)
        {
            window.addEventListener("load", function() { sh_highlightDocument(); document.getElementById("verifycode_guid").value = ""; }, false);
        }

    </script>

</asp:Content>
