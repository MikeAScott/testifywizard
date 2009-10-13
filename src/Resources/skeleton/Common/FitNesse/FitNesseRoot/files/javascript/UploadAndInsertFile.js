
function endsWith(str, s){
	var reg = new RegExp (s + "$");
	return reg.test(str);
}
var imageExtensions = [".jpg",".gif",".bmp"];
function InsertImageAfterUpload(fileUrl){
	var dot = fileUrl.lastIndexOf(".");
	if(dot>-1){
		var extension = fileUrl.substr(dot).toLowerCase();
		for(var i=0;i<imageExtensions.length;i++){
			if(extension==imageExtensions[i]){
				window.opener.FCK.InsertHtml('<img src="/' + fileUrl + '"/>');
				window.close();
				return;
			}
		}
		window.opener.FCK.InsertHtml('<a href="http://' + fileUrl + '">' + fileUrl + '</a>');
		window.close();
		return;
	}
	window.close();
}