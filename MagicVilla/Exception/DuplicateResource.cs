namespace MagicVilla;

public class DuplicateResource : Exception

{
	public DuplicateResource() { }
	public DuplicateResource(string message) : base(message) { }
	public DuplicateResource(string message, Exception inner) : base(message, inner) { }
}