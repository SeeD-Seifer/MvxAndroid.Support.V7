using System;
using Android.Content;
using Android.Views;
using MvvmCross.Binding.Droid.BindingContext;
using Android.Support.V7.Widget;

namespace MvxAndroid.Support.V7.Views
{
	public class MvxGenericRecyclerViewAdapter<T>: MvxRecyclerViewAdapter
										  where T: MvxRecyclerViewViewHolder
	{
		private readonly Func<View, IMvxAndroidBindingContext, T> factoryMethod;

		public MvxGenericRecyclerViewAdapter (Context context, IMvxAndroidBindingContext bindingContext,
			Func<View, IMvxAndroidBindingContext, T> factoryMethod)
			: base (context, bindingContext)
		{
			this.factoryMethod = factoryMethod;
		}

		public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var bindingContext = CreateBindingContextForViewHolder();

			View view = InflateViewForHolder(parent, viewType, bindingContext);

			var viewHolder = factoryMethod (view, bindingContext);

			viewHolder.Click = ItemClick;
			viewHolder.LongClick = ItemLongClick;

			return viewHolder;
		}
	}
}

