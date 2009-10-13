using System;

namespace ThoughtWorks.TreeSurgeon.Core.Generators
{
	public class StandardDotNetUpperCaseGuidGenerator : IGuidGenerator
	{
		public string GenerateGuid()
		{
			return Guid.NewGuid().ToString().ToUpper();
		}
	}
}