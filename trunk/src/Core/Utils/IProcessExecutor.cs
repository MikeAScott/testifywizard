namespace ThoughtWorks.TreeSurgeon.Core.Utils
{
	public interface IProcessExecutor
	{
		ProcessResult Execute(ProcessInfo processInfo);
	}
}