<%@ Control Language="C#" AutoEventWireup="true" Inherits="Home.CommandCtrl" %>

<script type="text/javascript" language="javascript">
	function DoCommand(command, data)
	{
		document.getElementById("<%= ClientID %>_command").value = command;
		document.getElementById("<%= ClientID %>_data").value = Core.Utility.RenderJson(data);
		document.getElementById("<%= Page.Form.UniqueID %>").submit();
	}

	function GetState()
	{
		try
		{
			if( <%= StateVarName %> == null)
			{
				<%= StateVarName %> = Core.Utility.ParseJson(document.getElementById("<%= ClientID %>_state_json").value);
			}
			return <%= StateVarName %>;
		}
		catch(ex)
		{
			return {};
		}
	}

	function ResetState(state)
	{
		<%= StateVarName %> = state;
		document.getElementById("<%= ClientID %>_state_json").value = Core.Utility.RenderJson(state);
	}
	
	var <%= StateVarName %> = null;
</script>

<input type="hidden" name="<%= ClientID %>_state_json" id="<%= ClientID %>_state_json" value="<%= StateJson %>" />
<input type="hidden" name="<%= ClientID %>_data" id="<%= ClientID %>_data" />
<input type="hidden" name="<%= ClientID %>_command" id="<%= ClientID %>_command" />

