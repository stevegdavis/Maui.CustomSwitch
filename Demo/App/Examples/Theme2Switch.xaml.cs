﻿using System.Windows.Input;
using CustomSwitch.Helpers;

namespace App.Examples;

public partial class Theme2Switch : ContentView
{
	public Theme2Switch()
	{
		InitializeComponent();
	}

	public event EventHandler<ToggledEventArgs>? Toggled = null;
	public static readonly BindableProperty IsToggledProperty = BindableProperty.Create(nameof(IsToggled), typeof(bool), typeof(Theme2Switch), false, BindingMode.TwoWay);
	public bool IsToggled
	{
		get => (bool)GetValue(IsToggledProperty);
		set => SetValue(IsToggledProperty, value);
	}

	public static readonly BindableProperty ToggledCommandProperty = BindableProperty.Create(nameof(ToggledCommand), typeof(ICommand), typeof(Theme2Switch));
	public ICommand ToggledCommand
	{
		get => (ICommand)GetValue(ToggledCommandProperty);
		set => SetValue(ToggledCommandProperty, value);
	}

	void CustomSwitch_SwitchPanUpdate(object sender, CustomSwitch.Events.SwitchPanUpdatedEventArgs e)
	{
		Color fromColorGradient1 = e.IsToggled ? Color.FromArgb("#16d4f4") : Color.FromArgb("#4a467e");
		Color toColorGradient1 = e.IsToggled ? Color.FromArgb("#4a467e") : Color.FromArgb("#16d4f4");

		Color fromColorGradient2 = e.IsToggled ? Color.FromArgb("#cffdfc") : Color.FromArgb("#21103a");
		Color toColorGradient2 = e.IsToggled ? Color.FromArgb("#21103a") : Color.FromArgb("#cffdfc");

		double t = e.Percentage * 0.01;

		Flex.TranslationX = -(e.TranslateX + e.XRef);
		if (e.IsToggled)
		{
			if (e.Percentage >= 50)
			{
				MoonImg.Opacity = (e.Percentage - 50) * 2 * 0.01;
			}
		}
		else
		{
			if (e.Percentage <= 50)
			{
				MoonImg.Opacity = (100 - (e.Percentage * 2)) * 0.01;
			}
		}

		CustomSwitch.CustomSwitch customSwitch = (CustomSwitch.CustomSwitch)sender;
		customSwitch.Background = new LinearGradientBrush(new GradientStopCollection
		{
			new GradientStop
			{
				Color =  ColorAnimationUtil.ColorAnimation(fromColorGradient1, toColorGradient1, t),
				Offset = 0
			},
			new GradientStop
			{
				Color = ColorAnimationUtil.ColorAnimation(fromColorGradient2, toColorGradient2, t),
				Offset = 1
			}
		});
	}
	void CustomSwitch_Toggled(object sender, ToggledEventArgs e)
	{
		Toggled?.Invoke(sender, e);
	}
}