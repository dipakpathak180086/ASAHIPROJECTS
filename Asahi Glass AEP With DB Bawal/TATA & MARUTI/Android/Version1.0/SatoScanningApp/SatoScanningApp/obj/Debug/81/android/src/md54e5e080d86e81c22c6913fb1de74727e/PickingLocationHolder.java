package md54e5e080d86e81c22c6913fb1de74727e;


public class PickingLocationHolder
	extends android.support.v7.widget.RecyclerView.ViewHolder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("AISScanningApp.Adapter.PickingLocationHolder, AISScanningApp", PickingLocationHolder.class, __md_methods);
	}


	public PickingLocationHolder (android.view.View p0)
	{
		super (p0);
		if (getClass () == PickingLocationHolder.class)
			mono.android.TypeManager.Activate ("AISScanningApp.Adapter.PickingLocationHolder, AISScanningApp", "Android.Views.View, Mono.Android", this, new java.lang.Object[] { p0 });
	}

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
