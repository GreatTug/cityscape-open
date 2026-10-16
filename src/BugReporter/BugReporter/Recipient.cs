namespace BugReporter;

public class Recipient
{
	public string Address;

	public string DisplayName;

	public MapiMailMessage.RecipientType RecipientType = MapiMailMessage.RecipientType.To;

	public Recipient(string address)
	{
		Address = address;
	}

	public Recipient(string address, string displayName)
	{
		Address = address;
		DisplayName = displayName;
	}

	public Recipient(string address, MapiMailMessage.RecipientType recipientType)
	{
		Address = address;
		RecipientType = recipientType;
	}

	public Recipient(string address, string displayName, MapiMailMessage.RecipientType recipientType)
	{
		Address = address;
		DisplayName = displayName;
		RecipientType = recipientType;
	}

	internal MapiMailMessage.MAPIHelperInterop.MapiRecipDesc GetInteropRepresentation()
	{
		MapiMailMessage.MAPIHelperInterop.MapiRecipDesc mapiRecipDesc = new MapiMailMessage.MAPIHelperInterop.MapiRecipDesc();
		if (DisplayName == null)
		{
			mapiRecipDesc.Name = Address;
		}
		else
		{
			mapiRecipDesc.Name = DisplayName;
			mapiRecipDesc.Address = Address;
		}
		mapiRecipDesc.RecipientClass = (int)RecipientType;
		return mapiRecipDesc;
	}
}
