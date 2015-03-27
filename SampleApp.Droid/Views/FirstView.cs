using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Cirrious.MvvmCross.Droid.Views;
using MvxAndroid.Support.V7.Views;
using SampleApp.Core.ViewModels;
using Android.Views;
using Cirrious.MvvmCross.Binding.Droid.BindingContext;
using Android.Content;
using System;

namespace SampleApp.Views
{
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity<FirstViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);

            var recyclerView = FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);
            recyclerView.HasFixedSize = true;

			var bindingContext = ((MvxRecyclerViewAdapter)recyclerView.MvxAdapter).BindingContext;
			recyclerView.MvxAdapter = new MvxGenericRecyclerViewAdapter<CustomViewHolder> (this, bindingContext, FactoryMethod);

            var layoutManager = new LinearLayoutManager(this);
            recyclerView.SetLayoutManager(layoutManager);
        }

		private CustomViewHolder FactoryMethod (View view, IMvxAndroidBindingContext bindingContext)
		{
			return new CustomViewHolder (view, bindingContext)
			{
				CustomViewClicked = OnCustomViewClicked
			};
		}

		private void OnCustomViewClicked ()
		{
			// TODO handle custom view click
			// For example expand additional area
		}

		private class CustomViewHolder: MvxRecyclerViewViewHolder
		{
			private readonly View customView;

			public Action CustomViewClicked;

			public CustomViewHolder (View itemView, IMvxAndroidBindingContext bindingContext)
				:base (itemView, bindingContext)
			{
				// Find custom view here, so that it is possible to subscribe on internal Click (or other) events
				//customView = itemView.FindViewById (Resource.Id.customView);
				customView.Click += delegate { CustomViewClicked(); };
			}
		}
    }
}