<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
    <xsl:output method="xml" indent="yes"/>

	<xsl:template match="/">
		<html>
			<body>
				<h2>Розклад роботи дисплейних класів</h2>
				<table border="1">
					<tr bgcolor="yellow">
						<th style="text-align:center">Номер кабінету</th>
						<th style="text-align:center">Кількість місць</th>
						<th style="text-align:center">Розклад</th>
					</tr>
					<xsl:for-each select="root/Class">
						<tr>				
							<td style="text-align:center">
								<xsl:value-of select="@ClassName"/>
							</td>
							<td style="text-align:center">
								<xsl:value-of select="@SeatsNum"/>
							</td>
							<td>
								<xsl:for-each select="Day">
										<p>>&#160;<xsl:value-of select="@DayName"/><br/>
									<xsl:for-each select="Pair">
										&#160;&#160;&#160;&#160;&#160;пара <xsl:value-of select="@PairNum"/>
										 - викладач: <xsl:value-of select="@Professor"/><br/>			
									</xsl:for-each>
										</p>
								</xsl:for-each>
							</td>
						</tr>
					</xsl:for-each>
				</table>
			</body>
		</html>
	</xsl:template>
</xsl:stylesheet>
