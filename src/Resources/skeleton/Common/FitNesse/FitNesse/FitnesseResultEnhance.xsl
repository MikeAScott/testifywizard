<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
	<xsl:output method="xml"/>
	<xsl:param name="fit.host" />
	<xsl:param name="fit.port" />
	<xsl:param name="fit.root" />
	<xsl:param name="suite.name" />

	<xsl:template match="/">
		<xsl:apply-templates select="//testResults" />
	</xsl:template>

	<xsl:template match="testResults">
		<testResults>
			<fitnesseHost><xsl:value-of select="$fit.host"/></fitnesseHost>
			<fitnessePort><xsl:value-of select="$fit.port"/></fitnessePort>
			<fitnesseRoot><xsl:value-of select="$fit.root"/></fitnesseRoot>
			<suiteName><xsl:value-of select="$suite.name"/></suiteName>
			<xsl:apply-templates select="@*|node()"/>
		</testResults>
	</xsl:template>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>


</xsl:stylesheet>