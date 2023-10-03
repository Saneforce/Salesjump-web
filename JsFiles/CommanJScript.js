<SCRIPT LANGUAGE="JavaScript">
function _fNvALIDeNTRY(_tYPE,_MaxL)
{
    var _t=true;
    var _f=false;
    var _cTRL=event.srcElement;
    var _v=_cTRL.value;    
    if(_tYPE=='N' || _tYPE=='n' || _tYPE=='D' || _tYPE=='d' || _tYPE=='T' || _tYPE=='t')
    {
        if((event.keyCode>=48 && event.keyCode<=57) || event.keyCode==46)
        {
            if (removeSpace(_v)=='' && event.keyCode==46)
            {
                event.returnValue = _f;
                return _f;
            }            
            if ((_tYPE=='D' || _tYPE=='d'))
            {
                var _sTi=_v.indexOf('.');
                if(_sTi<=-1)
                {
                    if(_v.length<_MaxL-2 || event.keyCode==46)
                    {
                        event.returnValue = _t;
                        return _t;
                    }
                }
            }
            if ((_v.substring(_sTi+1,_v.length).length!=2 || _tYPE=='N' || _tYPE=='n') && (event.keyCode!=46 && event.keyCode!=47 && event.keyCode!=58 ))
            {
                if(_v.length<_MaxL ||_sTi>-1 )
                {
                    event.returnValue = _t;
                    return _t;
                }
            }
        }
    }
    else if(_tYPE=='C' || _tYPE=='c')
    {
        if(_v.length<_MaxL)
        {
            event.returnValue = _t;
            return _t;
        }
    }
    else if(_tYPE.substring(0,3)=='-O-' || _tYPE.substring(0,3)=='-o-')
    {
        if(_v.length<_MaxL)
        {
            _tYPE=_tYPE.substring(3,_tYPE.length);
            var _C=String.fromCharCode(event.keyCode);
            if (_C=='"') 
                _tYPE=_tYPE.replace("!!",'"');
            if (_C=="'") 
                _tYPE=_tYPE.replace("~~","'");
            if(_tYPE.indexOf(_C)==-1)
            {
                event.returnValue = _t;
                return _t;
            }
        }
    }
    event.returnValue = _f;
}
function AllowOnlyNumbers()
{
    if((event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode==13)
    {
        return true;
    }
    else    
    {
        event.keyCode = 0;
        return false;
    }
}

//Function AllowOnlyChars() will acceptd All Upper Case (A-Z), All Lower Case (a-z), Enter key & BAckspace key
function AllowOnlyChars()
{
        if(!((event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90) || event.keyCode==32 || event.keyCode==95 || event.keyCode==13))
        {			
            event.keyCode = 0;
            event.returnValue=false;		
        }
        else
        {
            return true;
        }        
}
function AllowCharswithfewspl()
{
        if(!((event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90) || event.keyCode==32 || event.keyCode==95 || event.keyCode==13 ||event.keyCode==43 || event.keyCode==95 ||event.keyCode==45||event.keyCode==60||event.keyCode==61||event.keyCode==62))
        {			
            event.keyCode = 0;
            event.returnValue=false;		
        }
        else
        {
            return true;
        }
   }
function suppressspecialcharacters() 
{
   if(event.keyCode>=33 && event.keyCode<=47)
   {
		event.keyCode=false;
   }
   //if(event.keyCode>37 && event.keyCode<=45)
    //{
	//event.keyCode=false;
   // }  
    else if(event.keyCode>=91 && event.keyCode<=96)
   {
		event.keyCode=false;
   }
   else if(event.keyCode>=58 && event.keyCode<=64)
   {
		event.keyCode=false;
   }
   else if(event.keyCode>=123 && event.keyCode<=126)
   {
		event.keyCode=false;
   }      
   else if(event.keyCode==39)
   {
		event.returnValue = false;
   }
}
function suppresscharactersaddress()
{
//alert(event.keyCode)
   if(event.keyCode>=33 && event.keyCode<=43)
   {
		event.keyCode=false;
   }
   else if(event.keyCode>=91 && event.keyCode<=96)
   {
		event.keyCode=false;
   }
   else if(event.keyCode>=58 && event.keyCode<=64)
   {
		event.keyCode=false;
   }
   else if(event.keyCode>=123 && event.keyCode<=126)
   {
		event.keyCode=false;
   }      
   else if(event.keyCode==45)
   {
		event.returnValue = false;
   }
//   else if(event.keyCode==47)
//   {
//		event.returnValue = false;
//   }   
}
function initNoSpace()
{
_$e=event;
	with(_$e)	
	{	
		var s=srcElement.value		
		var CurrPos=Math.abs(document.selection.createRange().moveStart("character", -1000000));
		var end = CurrPos 		
		setSelectionRange(srcElement,CurrPos,end)
		if ((CurrPos==0 || s.substring(CurrPos-1,CurrPos)==' ' || s.substring(CurrPos,CurrPos+1)==' ') && keyCode==32){keyCode=0;return false;}
	}
}
  function SetProperCase()
{	_$e=event;
	with(_$e)	
	{	
		var s=srcElement.value		
		var CurrPos=Math.abs(document.selection.createRange().moveStart("character", -1000000));
		var end = CurrPos 		
		setSelectionRange(srcElement,CurrPos,end)
		if ((CurrPos==0 || s.substring(CurrPos-1,CurrPos)==' ' || s.substring(CurrPos,CurrPos+1)==' ') && keyCode==32){keyCode=0;return false;}
		if(keyCode==32)
		{
			srcElement.value =s.substring(0,CurrPos)+s.substring(CurrPos,CurrPos+1).toUpperCase()+s.substring(CurrPos+1,s.length);
			setSelectionRange(srcElement,CurrPos,end)
		}
		if (s.substring(CurrPos-1,CurrPos)=='' || s.substring(CurrPos-1,CurrPos)==' ')
		{				
			if ((keyCode>=97 && keyCode<=122)) keyCode=keyCode-32				
			srcElement.value =s.substring(0,CurrPos)+s.substring(CurrPos,CurrPos+1).toLowerCase()+s.substring(CurrPos+1,s.length);
			setSelectionRange(srcElement,CurrPos,end)
		}
		else{if ((keyCode>=65 && keyCode<=90))keyCode=keyCode+32}
		event.srcElement.onkeydown=new Function("ChangeDelProper()")
		event.srcElement.onpaste=function(){return false;}		
	}
}
var is_gecko = /gecko/i.test(navigator.userAgent);
var is_ie    = /MSIE/.test(navigator.userAgent);
function ChangeDelProper()
{	_$e=event;
	with(_$e)	
	{		
		var s=srcElement.value
		var CurrPos=Math.abs(document.selection.createRange().moveStart("character", -1000000));				
		var end = Math.abs(document.selection.createRange().moveEnd("character", -1000000));		
		if (keyCode==46)
		{
			if (!(CurrPos==0 && end==s.length))
			{
			setSelectionRange(srcElement,CurrPos++,end++)
			keyCode=8
			}
		}
		if (keyCode==8)
		{
			srcElement.value =s.substring(0,CurrPos)+s.substring(CurrPos,CurrPos+1).toLowerCase()+s.substring(CurrPos+1,s.length);			
			setSelectionRange(srcElement,CurrPos,end)
		}		
	}
}

function setSelectionRange(input, start, end) {
	if (is_gecko) {
		input.setSelectionRange(start, end);
	} else {
		// assumed IE
		var range = input.createTextRange();
		range.collapse(true);
		range.moveStart("character", start);
		range.moveEnd("character", end - start);
		range.select();
	}
};
document.ondragstart =function (){return false}
  function NumericOnly()
    {
        if(!(event.keyCode>=48 && event.keyCode<=57))
        {			
            event.returnValue=false;		
        }
    }
    function AlphaNumeric()
    {
        if(!((event.keyCode>=48 && event.keyCode<=57) || (event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90) || event.keyCode==32 || event.keyCode==44 || event.keyCode==95 || event.keyCode==46 || event.keyCode==45 || event.keyCode==35 || event.keyCode==36 || event.keyCode==37 || event.keyCode==38))
        {			
            event.returnValue=false;		
        }
    }
    function CharactersOnly()
    {
        if(!((event.keyCode>=97 && event.keyCode<=122) || (event.keyCode>=65 && event.keyCode<=90) || event.keyCode==32 || event.keyCode==95))
        {			
            event.returnValue=false;		
        }
    }
function PhoneValid()
{
if(event.keyCode >= 48 && event.keyCode <= 57 || event.keyCode == 40  || event.keyCode == 41 || event.keyCode == 45   || event.keyCode==43 || event.keyCode==32 ){return true;}else{event.keyCode = 0;return false;}
}
    function TextWithoutSpecialCharacter()
    {
        if(!((keyCode>=97 && keyCode<=122) || (keyCode>=65 && keyCode<=90)))
        {			
            event.returnValue=false;
        }        
    }
//    function TextWithoutSpecialCharacter(keyCode)
//    {
//        
//        var isReturn =false;
//        if(keyCode>=65 && keyCode<=90)
//       //if((keyCode>=97 && keyCode<=122) || (keyCode>=65 && keyCode<=90))
//        {
//            isReturn = true;
//        }
//        return isReturn;
//    }
        var isShift = true;
        function isOnlyChar(keyCode) 
        {
             if(!((keyCode>=97 && keyCode<=122) || (keyCode>=65 && keyCode<=90)))
             {
                isShift = false;
                return isShift
             }
        }

</script>
 

