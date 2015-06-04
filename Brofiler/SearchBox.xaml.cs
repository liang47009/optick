﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace Profiler
{

	/// <summary>
	/// Interaction logic for SearchBox.xaml
	/// </summary>
	public partial class SearchBox : UserControl
	{
		public SearchBox()
		{
			InitializeComponent();

			delayedTextUpdateTimer.Elapsed += new ElapsedEventHandler(OnDelayedTextUpdate);
			delayedTextUpdateTimer.AutoReset = false;
		}

		private void FilterText_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (!isFiltering)
				return;

			delayedTextUpdateTimer.Stop();
			delayedTextUpdateTimer.Start();
		}

		bool isFiltering = false;
		private void FilterText_GotFocus(object sender, RoutedEventArgs e)
		{
			if (!isFiltering)
			{
				FilterText.Text = String.Empty;
				isFiltering = true;
			}
		}

		Timer delayedTextUpdateTimer = new Timer(300);

		public String Text { get { return FilterText.Text; } }

		void OnDelayedTextUpdate(object sender, ElapsedEventArgs e)
		{
			Application.Current.Dispatcher.BeginInvoke(new Action(() => { DelayedTextChanged(FilterText.Text); }));
		}

		public delegate void DelayedTextChangedEventHandler(String text);
		public event DelayedTextChangedEventHandler DelayedTextChanged;

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			FilterText.Text = String.Empty;
		}
	}
}
