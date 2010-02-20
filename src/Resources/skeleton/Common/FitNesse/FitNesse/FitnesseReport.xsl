<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:output method="html"/>
	<xsl:param name="fit.host" />
	<xsl:param name="fit.port" />
	<xsl:param name="fit.root" />
	<xsl:param name="suite.name" />
<xsl:template match="/">
	<html>
<head>
<title><xsl:value-of select="$suite.name" /></title>
<base href="http://{$fit.host}:{$fit.port}" ></base>
		<link rel="stylesheet" type="text/css" href="/files/css/fitnesse.css" media="screen"/> 
		<link rel="stylesheet" type="text/css" href="/files/css/fitnesse_print.css" media="print"/> 
		<script src="/files/javascript/fitnesse.js" type="text/javascript"></script> 

<style>
	*.pass{
		background-color: #AAFFAA;
	}
	*.fail{	
		background-color: #FFAAAA;
	}
	*.fit_label {	
		color: #AAAAAA;
	}
	*.error{
		background-color: #FFFFAA;	
	}
	*.fitignore{
		background-color: #CCCCCC;	
	}
	*.fitheader{	
		border: solid 1px black;
		margin: 1px;
		padding: 2px;
	}
	*.line{	
		margin: 5px;
	}
</style>
<script>
	function toggleDiv( imgId, divId )
	{
		eDiv = document.getElementById( divId );
		eImg = document.getElementById( imgId );
		
		if ( eDiv.style.display == "none" )
		{
			eDiv.style.display = "block";
//			eImg.src = "images/arrow_minus_small.gif";
			eImg.src = "/files/images/collapsableOpen.gif";
		}
		else
		{
			eDiv.style.display = "none";
//			eImg.src = "images/arrow_plus_small.gif";
			eImg.src = "/files/images/collapsableClosed.gif";
		}
	}
	
	function exportDivToNewWindow( divId )
	{
		eDiv = document.getElementById( divId );
		newWin = window.open("", "_new");
		newWin.document.write("&lt;link href='fitnesse.css' type='text/css' rel='stylesheet'&gt;");
		newWin.document.write(eDiv.innerHTML);
		newWin.document.close();
	}	
</script>
</head>
<body>
<div>
	<xsl:apply-templates select="//testResults" />
</div>
</body>
</html>
</xsl:template>
<xsl:template match="testResults">

<xsl:variable name="fit.result.list" select="result" />
<xsl:variable name="fit.wrongpagecount" select="count($fit.result.list/counts/wrong[text() > 0])" />
<xsl:variable name="fit.ignorespagecount" select="count($fit.result.list/counts/ignores[text() > 0])" />
<xsl:variable name="fit.exceptionspagecount" select="count($fit.result.list/counts/exceptions[text() > 0])" />
<xsl:variable name="fit.correctpagecount" select="count($fit.result.list/counts[wrong/text() = 0 and exceptions/text() = 0 and ignores/text() = 0])" />
<xsl:variable name="fit.correctcount" select="finalCounts/right"/>
<xsl:variable name="fit.failures" select="finalCounts/wrong"/>
<xsl:variable name="fit.notrun" select="finalCounts/ignores"/>
<xsl:variable name="fit.exceptions" select="finalCounts/exceptions"/>
<xsl:variable name="fit.case.list" select="$fit.result.list//test-case"/>
<xsl:variable name="fit.suite.list" select="$fit.result.list//test-suite"/>
<xsl:variable name="fit.failure.list" select="$fit.case.list//failure"/>
<xsl:variable name="fit.notrun.list" select="$fit.case.list//reason"/>
<xsl:variable name="headerColorClass">
	<xsl:choose>
		<xsl:when test="$fit.exceptionspagecount > 0">error</xsl:when>
		<xsl:when test="$fit.ignorespagecount > 0">fitignore</xsl:when>
		<xsl:when test="$fit.wrongpagecount > 0" >fail</xsl:when>
		<xsl:otherwise>pass</xsl:otherwise>
	</xsl:choose>
</xsl:variable>
<div class="{$headerColorClass} fitheader">
	<strong>FitNesse Report -- <xsl:value-of select="$fit.host"/>:<xsl:value-of select="$fit.port"/>/<xsl:value-of select="$suite.name"/> -- Test Pages: </strong>
	<xsl:value-of select="$fit.correctpagecount"/> right, <xsl:value-of select="$fit.wrongpagecount"/> wrong,
	<xsl:value-of select="$fit.ignorespagecount"/> ignored, <xsl:value-of select="$fit.exceptionspagecount"/> exceptions
	<strong>Assertions: </strong> 
	<xsl:value-of select="$fit.correctcount"/> right, 
	<xsl:value-of select="$fit.failures"/> wrong,
	<xsl:value-of select="$fit.notrun"/> ignored, 
	<xsl:value-of select="$fit.exceptions"/> exceptions
</div>
<xsl:for-each select="$fit.result.list">
	<xsl:variable name="test.id" select="generate-id()" />
	<xsl:variable name="test.name">
		<xsl:choose>
			<xsl:when test="starts-with(relativePageName,$fit.root)">
				<xsl:value-of select="relativePageName"/>
			</xsl:when>
			<xsl:otherwise>
				<xsl:value-of select="$suite.name"/>.<xsl:value-of select="relativePageName"/>
			</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<xsl:variable name="colorClass">
		<xsl:choose>
			<xsl:when test="counts/exceptions > 0">error</xsl:when>
			<xsl:when test="counts/ignores > 0">fitignore</xsl:when>
			<xsl:when test="counts/wrong > 0" >fail</xsl:when>
			<xsl:otherwise>pass</xsl:otherwise>
		</xsl:choose>
	</xsl:variable>
	<div class="line">
		<span class="{$colorClass}" style="padding:2px;" >
			<xsl:value-of select="counts/right"/> right, <xsl:value-of select="counts/wrong"/> wrong,
			<xsl:value-of select="counts/ignores"/> ignored, <xsl:value-of select="counts/exceptions"/> exceptions
		</span>
		<input type="image" src="/files/images/collapsableClosed.gif">
			<xsl:attribute name="id">img<xsl:value-of select="$test.id"/></xsl:attribute>
			<xsl:attribute name="onclick">javascript:toggleDiv('img<xsl:value-of select="$test.id"/>', 'fitDetails<xsl:value-of select="$test.id"/>');</xsl:attribute>
		</input>&#160;
		<input type="button">
			<xsl:attribute name="value">Export Details</xsl:attribute>
			<xsl:attribute name="onclick">javascript:exportDivToNewWindow('fitDetails<xsl:value-of select="$test.id"/>');</xsl:attribute>
		</input>&#160;
		<a href="{$test.name}"><xsl:value-of select="$test.name"/></a>
	</div>
	<span style="display:none">
		<xsl:attribute name="id">fitDetails<xsl:value-of select="$test.id"/></xsl:attribute>
		<blockquote>
			<xsl:value-of select="content" disable-output-escaping="yes"/>
		</blockquote>
	</span>
</xsl:for-each>
</xsl:template>

</xsl:stylesheet>