function validate() 
{
	Code = document.frmComment_Master.txtComment_Code.value;
	
	if (Code == "" || Code.charAt(0)==" " )
	{
	
		alert("Enter Comment Code ");
		document.frmComment_Master.txtComment_Code.focus();
		return false   
	}
	
	Name = document.frmComment_Master.txtComment_Name.value;
	
	if (Name == "" || Name.charAt(0)==" " )
	
	{
		alert("Enter Comment Name ");
		document.frmComment_Master.txtComment_Name.focus();
		return false   
	}
	
	Value = document.frmComment_Master.selComment_Type.value;
	
	if (Value == "" || Value.charAt(0)==" " )
	{
		alert("Enter Comment Type ");
		document.frmComment_Master.selComment_Type.focus();
		return false   
	}
	
	return true
}

function jspage(intpage)
{
	
	document.frmComment_list.disppage.value = intpage;
	document.frmComment_list.submit();
}



function confirmDelete()
{
	var confirmMesg

	confirmMesg = confirm("Are You Sure to Delete Record")

	if (confirmMesg == true)
	{	
		return true;
	}
	else
	{
		return false;
	}	
		
}