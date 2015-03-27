using System.Collections;
using System.Windows.Input;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Util;
using Cirrious.MvvmCross.Binding.Attributes;
using Cirrious.MvvmCross.Binding.Droid.Views;

namespace MvxAndroid.Support.V7.Views
{
    public class MvxRecyclerView : RecyclerView
    {
        public MvxRecyclerView(Context context, IAttributeSet attrs)
            : this(context, attrs, new MvxRecyclerViewAdapter(context))
        { }

        public MvxRecyclerView(Context context, IAttributeSet attrs, IMvxRecyclerViewAdapter adapter)
            : base(context, attrs)
        {
            if (adapter == null)
                return;

            var itemTemplateId = MvxAttributeHelpers.ReadListItemTemplateId(context, attrs);
            adapter.ItemTemplateId = itemTemplateId;
			this.MvxAdapter = adapter;
        }

        public IMvxRecyclerViewAdapter MvxAdapter
        {
            get { return this.GetAdapter(); }
            set { this.SetAdapter(value); }
        }

        public new IMvxRecyclerViewAdapter GetAdapter()
        {
            return base.GetAdapter() as IMvxRecyclerViewAdapter;
        }

        public virtual void SetAdapter(IMvxRecyclerViewAdapter adapter)
        {
            var existing = this.GetAdapter();
            if (existing == adapter)
                return;

            if (adapter != null && existing != null)
            {
                adapter.ItemsSource = existing.ItemsSource;
                adapter.ItemTemplateId = existing.ItemTemplateId;
                adapter.ItemClick = existing.ItemClick;
            }
            base.SetAdapter((Adapter)adapter);
        }

        [MvxSetToNullAfterBinding]
        public IEnumerable ItemsSource
        {
			get { return this.MvxAdapter.ItemsSource; }
			set { this.MvxAdapter.ItemsSource = value; }
        }

        public int ItemTemplateId
        {
			get { return this.MvxAdapter.ItemTemplateId; }
			set { this.MvxAdapter.ItemTemplateId = value; }
        }

        public ICommand ItemClick
        {
			get { return this.MvxAdapter.ItemClick; }
			set { this.MvxAdapter.ItemClick = value; }
        }

        public ICommand ItemLongClick
        {
			get { return this.MvxAdapter.ItemLongClick; }
			set { this.MvxAdapter.ItemLongClick = value; }
        }
    }
}