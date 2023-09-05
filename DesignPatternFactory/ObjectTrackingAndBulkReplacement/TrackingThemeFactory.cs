using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatternFactory.ObjectTrackingAndBulkReplacement
{
    public class TrackingThemeFactory
    {
        private readonly List<WeakReference<ITheme>> themes = new List<WeakReference<ITheme>>();
        public ITheme CreateTheme(bool dark)
        {
            ITheme theme = dark ? new DarkTheme() : new LightTheme();
            themes.Add(new WeakReference<ITheme>(theme));

            return theme;
        }
        public string Info
        {
            get
            {
                var sb = new StringBuilder();
                foreach (var reference in themes)
                {
                    if (reference.TryGetTarget(out var theme))
                    {
                        bool dark = theme is DarkTheme;
                        sb.Append(dark ? "Dark" : "Light")
                            .AppendLine(" theme");
                    }
                }
                return sb.ToString();
            }
        }
    }
}
