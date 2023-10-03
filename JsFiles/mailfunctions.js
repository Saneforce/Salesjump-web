<script language="javascript">

function get_Addrow_id(obj)
{
	var id,len,i,s,rid;
	id=event.srcElement.id;
	len=id.length;
	idst=id.indexOf("_");
	while(idst!=-1)
	{
		rid=id.substring(idst+1,len);
		id=id.substring(idst+1,len);
		len=id.length;
		idst=id.indexOf("_");
	}
	return(rid);
}



function get_row_id(obj)
{
	var id,len,i,s,rid;
	id=obj.id;
	len=id.length;
	idst=id.indexOf("_");
	if(idst<0) return("");
	rid=id.substring(idst,len);
	return(rid);
}

//Used to get the first character in the given string 

function firstChar(val)
	{
	   var fc,len,ind
		len=val.length
		fc=""
		fc=val.substring(0,1)
		ind=val.indexOf(" ")
		while (ind!=-1)	
		{
			if (val.charAt(ind+1)!=' ') fc=fc+val.charAt(ind+1)
			val=val.substring(ind+1,len)
			len=val.length
			ind=val.indexOf(" ")
		}	
		return(fc);
	}


function AddRow(src_table,dest_table,Level_Row,Level_Str)
{
	
	
	frmtable=document.all(dest_table);
	
	frmtbody=frmtable.children[0];
	table=document.all(src_table);
	tbody=table.children[0];
	for(TRidx=0;TRidx<tbody.children.length;TRidx++)
	{
		TR=tbody.children[TRidx].cloneNode(true);
		for(TDidx=0;TDidx<TR.children.length;TDidx++)
		{
			TD=TR.children[TDidx];
			for(OBJidx=0;OBJidx<TD.children.length;OBJidx++)
			{
				OBJ=TD.children[OBJidx];
				OBJ.id=OBJ.id+'_'+Level_Str+Level_Row;
			}
		}
		frmtbody.insertAdjacentElement("beforeEnd",TR);
	}
	Level_Row++;
}

function DelRow(src_table,dest_table,l)
{
	
	if(l>1)
	{
		if(confirm("Do you want to delete the Row"))
		{
			srctbl=document.all(src_table);
			tbody=srctbl.children[0];
			TRs=tbody.children;
			TRnos=TRs.length;
			bt=event.srcElement;
			partd=bt.parentElement;
			partr=partd.parentElement;
			elem=partr;
 	
			for(i=0;i<TRnos;i++)
			{
				nextsib=elem.nextSibling;
				elem.removeNode(true);
				elem=nextsib;
			}
			return true
		}
		else
		{
			return false
		}
	}
}

function getObjectByDoc(ref,doc)
{
	obj=doc.getElementById?doc.getElementById(ref):doc.all?doc.all(ref):null;
	if(obj==null) 
	{
		alert(ref + ' is not an object');
		return(null);
	}
	else
	{
		return(obj);
	}
}

function getObject(ref)
{
	obj=document.getElementById?document.getElementById(ref):document.all?document.all(ref):null;
	if(obj==null) 
	{
		alert(ref + ' is not an object');
		return(null);
	}
	else
	{
		return(obj);
	}
}

function Del_N_Rows_ByObject(table,obj,n)
{			
			var bt=obj;
			var partd=bt.parentElement;
			var partr=partd.parentElement;
			var elem=partr;
			var i;
			for(i=0;i<n;i++)
			{
				nextsib=elem.nextSibling;
				elem.removeNode(true);
				elem=nextsib;
				if(elem==null) break;
			}
	
}


function SampAddRow(src_table,dest_table,Level_Row,Level_Str)
{
	frmtable=document.all(dest_table);
	frmtbody=frmtable.children[0];
	table=document.all(src_table);
	tbody=table.children[0];
	for(TRidx=0;TRidx<tbody.children.length;TRidx++)
	{
		TR=tbody.children[TRidx];
		TR1=document.createElement("TR");
		for(TDidx=0;TDidx<TR.children.length;TDidx++)
		{
			TD=TR.children[TDidx];
			TD1=document.createElement("TD");
			for(OBJidx=0;OBJidx<TD.children.length;OBJidx++)
			{
				OBJ=TD.children[OBJidx];
				if ((OBJ.type!="radio") && (OBJ.type!="checkbox"))
				{
					OBJ1=TD.children[OBJidx].cloneNode(true);
					OBJ1.id=OBJ1.id+'_'+Level_Str+Level_Row;
					TD1.appendChild(OBJ1)
					
				}
				else
				{
					str="<input "
					OBJ1=TD.children[OBJidx].cloneNode(true);
					att=OBJ1.attributes;
					for (i = 0; i < att.length; i++)
					{
						attr = att.item(i);
						aSpecified = attr.nodeName;
						bSpecified = attr.nodeValue;
						if ((aSpecified!=null) && (bSpecified!=null) && (aSpecified!="") && (bSpecified!=""))
						{
							if (aSpecified=="name")
							{
								bstr=bSpecified.split("#")
								str=str+aSpecified+"="+bstr[0]+Level_Row+" "
								str1=bstr[0].substring(3,bstr[0].length)
								nam=bstr[0]
							}	
							else	
							{	
								if (aSpecified=="id")
									str=str+aSpecified+"="+bSpecified+"_"+Level_Str+Level_Row+" "
								else	
									str=str+aSpecified+"="+bSpecified+" "	
							}		
						}
					}
					str=str+"  value="+ bstr[1]+"  onclick=addrow"+nam+"Fn(this)>"
					
					OBJ=document.createElement(str)
					TD1.appendChild(OBJ)
				}
			}
			TR1.appendChild(TD1)
		}
		frmtbody.insertAdjacentElement("beforeEnd",TR1);
	}
	Level_Row++;
}




function Del_Next_N_Rows_ByObject(table,obj,n)
{
	var bt=obj;
	var partd=bt.parentElement;
	var partr=partd.parentElement;
	var elem=partr;
	var i;
	elem=elem.nextSibling;	
	if(elem!=null)
	{		
		for(i=0;i<n;i++)
		{
			nextsib=elem.nextSibling;
			elem.removeNode(true);
			elem=nextsib;
			if(elem==null) break;
		}
	}
}

function DelRow_ByObject(src_table,dest_table,obj)
{
			srctbl=document.all(src_table);
			tbody=srctbl.children[0];
			TRs=tbody.children;
			TRnos=TRs.length;
			bt=obj;
			partd=bt.parentElement;
			partr=partd.parentElement;
			elem=partr;
			for(i=0;i<TRnos;i++)
			{
				nextsib=elem.nextSibling;
				elem.removeNode(true);
				elem=nextsib;
			}
}








function caps(field)
{
	var arr,s,i;
	s=field.value;
	arr = s.split(" ");
	field.value = "";
	if(s!="")
	{
		for(i=0;i<arr.length;i++)
		field.value = field.value + arr[i].substring(0,1).toUpperCase() + arr[i].substring(1,arr[i].length)+" ";
	}
}

function capsOne(field)
{
	var arr,s,i;
	s=field.value;
	//arr = s.split(" ");
	arr = field.value;
	arr1 = "";
	field.value = "";
	if(s!="")
	{
	   arr1 = arr.substring(1,arr.length);
	   field.value = arr.substring(0,1).toUpperCase() +arr1;
	}
}

function caps(field)
{
	var arr,s,i;
	s=field.value;
	arr = s.split(" ");
	field.value = "";
	if(s!="")
	{
		for(i=0;i<arr.length;i++)
		field.value = field.value + arr[i].substring(0,1).toUpperCase() + arr[i].substring(1,arr[i].length)+" ";
	}
}

function capsOne(field)
{
	var arr,s,i;
	s=field.value;
	//arr = s.split(" ");
	arr = field.value;
	arr1 = "";
	field.value = "";
	if(s!="")
	{
	   arr1 = arr.substring(1,arr.length);
	   field.value = arr.substring(0,1).toUpperCase() +arr1;
	}
}

function is_already_exist(obj,pos)
{
	var i=0;
	for(i=0;i<pos;i++)
	{
		if(obj[i].value==obj[pos].value)
			return(true);
	}
	return(false);
}

</script>