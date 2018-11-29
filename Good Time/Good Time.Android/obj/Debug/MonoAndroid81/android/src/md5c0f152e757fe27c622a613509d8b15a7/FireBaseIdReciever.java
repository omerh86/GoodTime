package md5c0f152e757fe27c622a613509d8b15a7;


public class FireBaseIdReciever
	extends com.google.firebase.iid.FirebaseInstanceIdService
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onTokenRefresh:()V:GetOnTokenRefreshHandler\n" +
			"";
		mono.android.Runtime.register ("Good_Time.Droid.FireBaseIdReciever, Good_Time.Droid", FireBaseIdReciever.class, __md_methods);
	}


	public FireBaseIdReciever ()
	{
		super ();
		if (getClass () == FireBaseIdReciever.class)
			mono.android.TypeManager.Activate ("Good_Time.Droid.FireBaseIdReciever, Good_Time.Droid", "", this, new java.lang.Object[] {  });
	}


	public void onTokenRefresh ()
	{
		n_onTokenRefresh ();
	}

	private native void n_onTokenRefresh ();

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
