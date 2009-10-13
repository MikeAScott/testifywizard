var RICHNESSE = "RichNesse";
var RICHEDITOR_BASEPATH = "/files/javascript/fckeditor/";
var FITNESSE_CONTROL = 'pageContent';
var DEBUG_RICHNESSE = true;
var CREATE_RICHEDIT_LINK = true;

function SetupRichnesseEditor(){
	var oFCKeditor = new FCKeditor(RICHNESSE);
	oFCKeditor.BasePath = RICHEDITOR_BASEPATH;
	oFCKeditor.Width = "100%";
	oFCKeditor.Height = clientHeight() - 120;
	oFCKeditor.Create();
}
function GetEditor(){
	return FCKeditorAPI.GetInstance(RICHNESSE);
}
function FCKeditor_OnComplete( editorInstance )
{
	setTimeout("FCKeditor_OnCompleteFreedThread()",200);
}
//in some cases, a undecipherable javascript error occurs if this code is called sequentially
function FCKeditor_OnCompleteFreedThread(){
	GetEditor().SetHTML(UnescapeRichText(document.getElementsByName(FITNESSE_CONTROL)[0].value));
	if(DEBUG_RICHNESSE){
		var SourceButton = document.getElementById('btnSource');
		if(SourceButton!=null)SourceButton.style.display = 'inline';	
	}
	var SaveAndEdit = document.getElementById('btnSaveAndEdit');
	if(SaveAndEdit!=null)SaveAndEdit.style.display = 'inline';
	var DiscardAndEdit = document.getElementById('btnDiscardAndEdit');
	if(DiscardAndEdit!=null)DiscardAndEdit.style.display = 'inline';
}
function f_filterResults(n_win, n_docel, n_body) {
	var n_result = n_win ? n_win : 0;
	if (n_docel && (!n_result || (n_result > n_docel)))
		n_result = n_docel;
	return n_body && (!n_result || (n_result > n_body)) ? n_body : n_result;
}
function clientHeight() {
	return f_filterResults (
		window.innerHeight ? window.innerHeight : 0,
		document.documentElement ? document.documentElement.clientHeight : 0,
		document.body ? document.body.clientHeight : 0
	);
}

var MAX_ADD_LINK_TRIES = 3;
var MILLISECOND_SLEEP = 200;
var tries = 0;
function GetAttributeValue(node,attribute){
	for(var i=0;i<node.attributes.length;i++){
		if(node.attributes[i].nodeName.toUpperCase()==attribute.toUpperCase())
			return node.attributes[i].nodeValue;
	}
	return "";
}
function SearchAndWaitForByAttribute(tagName,attributeName,attributeValue,retryCall){
	tries++;
	var elements = document.getElementsByTagName(tagName);
	for(var i=0;i<elements.length;i++){
		var value = GetAttributeValue(elements[i],attributeName);
		if(value==attributeValue){
			return elements[i];
		}
	}
	//still here, not found, try again.
	if(tries<MAX_ADD_LINK_TRIES){
		setTimeout(retryCall,MILLISECOND_SLEEP);
	}
}
function HighlightNextFailure(){
	//get current page selection
	//search for a failure after that (from top if not found)
	//when found, scroll to and highlight
	//<td class="fail">
	//<td class="error">
	//continuously search cells and tables in heirarchy until failure is found
	var failure = SearchAndWaitForByAttribute("td","class","fail");
	if(failure!=null){
		failure.scrollIntoView();		
		if(typeof(document.body.createTextRange)!=null){
			var rng=document.body.createTextRange();
			rng.moveToElementText(failure);
			rng.select(); 
		}
	}else{
		failure = SearchAndWaitForByAttribute("td","class","error");
		if(failure!=null){
			failure.scrollIntoView();		
			if(typeof(document.body.createTextRange)!=null){
				var rng=document.body.createTextRange();
				rng.moveToElementText(failure);
				rng.select(); 
			}
		}
	}	
}
function AddFailureFinder(){
	if(window.location.search == "?test"){
		var div = SearchAndWaitForByAttribute("div","class","actions",AddRichEditLink);
		var nextFailure = document.createElement('A');
		//nextFailure.onclick = HighlightNextFailure;
		nextFailure.innerHTML = "First Failure";
		nextFailure.href = "javascript:HighlightNextFailure()";
		div.appendChild(nextFailure);
	}
}
function AddRichEditLink(){
	if(CREATE_RICHEDIT_LINK){
		var editLink = SearchAndWaitForByAttribute("a","accesskey","e",AddRichEditLink);
		if(editLink==null)return;
		
		var div = SearchAndWaitForByAttribute("div","class","actions",AddRichEditLink);
		
		var richLink = document.createElement('A');
		richLink.innerHTML = "RichNesse";
		var pathname = location.pathname;
		if(pathname=="/")pathname += "FrontPage";
		richLink.href = pathname + "?RichEdit";
		div.insertBefore(richLink,editLink);
		
		AddFailureFinder();
	}
}
AddRichEditLink();
function ShowRichSource(){
	var textarea = '<textarea cols="160" rows="40">'+GetEditor().GetHTML()+'</textarea>';
	writeConsole(textarea);
}
function SaveAndEdit(){
	//add a redirect input to the form
	//set the value to the page + "?edit"
	//submit the form
	//maybe need a method to submit and view the xml to be parsed, and the resultant fit source, without saving
	// shown in Text Areas
	var redirect = document.createElement('INPUT');
	redirect.type = "hidden";
	redirect.id = "redirect";
	redirect.name = "redirect";
	redirect.value = location.pathname + '?edit';
	document.f.appendChild(redirect);
	document.f.submit();
}
function DiscardAndEdit(){
	var message = "All your edits will be lost! Are you sure you want to discard them?";
	if(confirm(message)){
		location = location.pathname + "?edit";
	}
}

function writeConsole(content) {
 top.consoleRef=window.open('','myconsole',
  'width=350,height=250'
   +',menubar=0'
   +',toolbar=1'
   +',status=0'
   +',scrollbars=1'
   +',resizable=1')
 top.consoleRef.document.writeln(
  '<html><head><title>Console</title></head>'
   +'<body bgcolor=white onLoad="self.focus()">'
   +content
   +'</body></html>'
 )
 top.consoleRef.document.close()
}
function UnescapeRichText(escapedRichText){
	return escapedRichText.replace(/%%%/g,'&lt;');
}