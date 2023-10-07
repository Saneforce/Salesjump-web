<SCRIPT LANGUAGE="JavaScript">
    function NumericOnly()
    {
        alert(event.keyCode);
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
</SCRIPT>