

var ToggleTableFormatting = function(){} 
ToggleTableFormatting.prototype.Execute = function()
{
	var aCells = FCKTableHandler.GetSelectedCells() ;

	// Check that just one cell is selected, otherwise return.
	if ( aCells.length != 1 )
		return ;
		
	var cell = aCells[0];
	var table = cell.parentNode.parentNode.parentNode; //row->tbody->table
	
	if(table.unformatted == "true" || table.unformatted == "" || table.unformatted == null){
		table.unformatted = "false";
		table.style.borderColor = "red";
	}else{
		table.unformatted = "true";
		table.style.borderColor = "black";
	}
	
}
// manage the plugins' button behavior  
ToggleTableFormatting.prototype.GetState = function()  
{  
	return FCK_TRISTATE_OFF;  
}  

FCKCommands.RegisterCommand( 'ToggleTableFormatting', new ToggleTableFormatting()) ;

// Create the ToggleTableFormatting toolbar button.
var button = new FCKToolbarButton( 'ToggleTableFormatting', 'Toggle Table Formatting' ) ;
button.IconPath = FCKConfig.PluginsPath + 'RichNesse/ToggleTableFormatting.gif' ;

FCKToolbarItems.RegisterItem( 'ToggleTableFormatting', button ) ; // 'ToggleTableFormatting' is the name used in the Toolbar config.

function popUp(URL) {
	day = new Date();
	id = day.getTime();
	eval("page" + id + " = window.open(URL, '" + id + "'," +  
	"'toolbar=0,scrollbars=1,location=0,statusbar=1,menubar=0,resizable=1,width=750,height=500');");
}

/**** UploadAndInsertFile ****/
var UploadAndInsertFile = function(){} 
UploadAndInsertFile.prototype.Execute = function()
{
	//void(win=window.open('/files/', 'UploadAndInsertImage', 
	//	'width=500,height=300,resizable=yes,scrollbars=no,menubar=no,status=no'));
	var arguments = null;
	//var returnValue = window.showModalDialog('/files/?ListDirectory', arguments, 'dialogHeight:500px;dialogWidth:750px;resizable:yes;');
	popUp('/files/?ListDirectory');
}
UploadAndInsertFile.prototype.GetState = function()  
{  
	return FCK_TRISTATE_OFF;  
}
FCKCommands.RegisterCommand( 'UploadAndInsertFile', new UploadAndInsertFile());
button = new FCKToolbarButton( 'UploadAndInsertFile', 'Upload and Insert File' );
button.IconPath = FCKConfig.PluginsPath + 'RichNesse/File.gif';
FCKToolbarItems.RegisterItem( 'UploadAndInsertFile', button );

