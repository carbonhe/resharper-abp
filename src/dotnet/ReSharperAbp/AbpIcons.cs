using JetBrains.Application.Icons.CompiledIconsCs;
using JetBrains.UI.Icons;
using JetBrains.Util.Icons;

namespace ReSharperAbp
{
    public sealed class AbpIcons
    {
        [CompiledIconCs]
        public sealed class Module : CompiledIconCsClass
        {
            public static IconId Id =
                new CompiledIconCsId(typeof(Module));

            public TiImage Load_Color()
            {
                return TiImageConverter.FromTiSvg(
                    @"<svg ti:v='1' width='16' height='16' viewBox='0,0,16,16' xmlns='http://www.w3.org/2000/svg' xmlns:ti='urn:schemas-jetbrains-com:tisvg'><g><path d='M673.8 903.8H532.2c-13.8 0-25-11.2-25-25v-41.5c0-15.2-5.9-29.5-16.7-40.3-10.8-10.8-25.1-16.7-40.3-16.7-31.4 0-57 25.6-57 57v41.5c0 13.8-11.2 25-25 25H226.8c-59 0-107-48-107-107V655.2c0-13.8 11.2-25 25-25h41.5c15.2 0 29.5-5.9 40.3-16.7 10.8-10.8 16.7-25.1 16.7-40.3 0-31.4-25.6-57-57-57h-41.5c-13.8 0-25-11.2-25-25V349.8c0-59 48-107 107-107h116.5v-15.5c0-59 48-107 107-107 28.6 0 55.4 11.1 75.7 31.3 20.2 20.2 31.3 47.1 31.3 75.7v15.5h116.5c59 0 107 48 107 107v116.5h16.5c59 0 107 48 107 107 0 28.6-11.1 55.4-31.3 75.7-20.2 20.2-47.1 31.3-75.7 31.3h-16.5v116.5c0 59-48 107-107 107z m-116.6-50h116.5c31.4 0 57-25.6 57-57V655.2c0-13.8 11.2-25 25-25h41.5c15.2 0 29.5-5.9 40.3-16.7 10.8-10.8 16.7-25.1 16.7-40.3 0-31.4-25.6-57-57-57h-41.5c-13.8 0-25-11.2-25-25V349.8c0-31.4-25.6-57-57-57H532.2c-13.8 0-25-11.2-25-25v-40.5c0-15.2-5.9-29.5-16.7-40.3-10.8-10.8-25.1-16.7-40.3-16.7-31.4 0-57 25.6-57 57v40.5c0 13.8-11.2 25-25 25H226.8c-31.4 0-57 25.6-57 57v116.5h16.5c59 0 107 48 107 107 0 28.6-11.1 55.4-31.3 75.7-20.2 20.2-47.1 31.3-75.7 31.3h-16.5v116.5c0 31.4 25.6 57 57 57h116.5v-16.5c0-59 48-107 107-107 28.6 0 55.4 11.1 75.7 31.3 20.2 20.2 31.3 47.1 31.3 75.7v16.5z' p-id='4282'></path></g></svg>");
            }

            public TiImage Load_Gray()
            {
                return TiImageConverter.FromTiSvg(
                    @"<svg ti:v='1' width='16' height='16' viewBox='0,0,16,16' xmlns='http://www.w3.org/2000/svg' xmlns:ti='urn:schemas-jetbrains-com:tisvg'><g><path d='M0,0L16,0L16,16L0,16Z' fill='#FFFFFF' opacity='0'/><path d='M15,6.338L13.372,0L12.46,0L6.686,1.613L5.919,3L4.408,3L0,7.598L0,8.402L4.408,13L5.919,13L6.686,14.387L12.46,16L13.372,16L15,9.662L15,9.278L14.293,8L15,6.722L15,6.338Z' fill='#F4F4F4'/><path d='M14,6.464L12.6,1L7.358,2.464L6.508,4L4.835,4L1,8L4.835,12L6.508,12L7.358,13.536L12.6,15L14,9.536L13.15,8ZM14,6.464M6.667,4.21L10.597999999999999,3.11L8.307,7.25L3.752,7.25ZM6.667,4.21M6.667,11.79L3.752,8.75L8.307,8.75L10.6,12.889ZM6.667,11.79M11.843,12.139L9.552,8L11.843,3.8609999999999998L12.906,8ZM11.843,12.139' fill='#323232'/></g></svg>");
            }

            public TiImage Load_GrayDark()
            {
                return TiImageConverter.FromTiSvg(
                    @"<svg ti:v='1' width='16' height='16' viewBox='0,0,16,16' xmlns='http://www.w3.org/2000/svg' xmlns:ti='urn:schemas-jetbrains-com:tisvg'><g><path d='M0,0L16,0L16,16L0,16Z' fill='#FFFFFF' opacity='0'/><path d='M15,6.338L13.372,0L12.46,0L6.686,1.613L5.919,3L4.408,3L0,7.598L0,8.402L4.408,13L5.919,13L6.686,14.387L12.46,16L13.372,16L15,9.662L15,9.278L14.293,8L15,6.722L15,6.338Z' fill='#252525'/><path d='M14,6.464L12.6,1L7.358,2.464L6.508,4L4.835,4L1,8L4.835,12L6.508,12L7.358,13.536L12.6,15L14,9.536L13.15,8ZM14,6.464M6.667,4.21L10.597999999999999,3.11L8.307,7.25L3.752,7.25ZM6.667,4.21M6.667,11.79L3.752,8.75L8.307,8.75L10.6,12.889ZM6.667,11.79M11.843,12.139L9.552,8L11.843,3.8609999999999998L12.906,8ZM11.843,12.139' fill='#C4C4C4'/></g></svg>");
            }

            public override
                CompiledIconCsIdOwner.ThemedIconThemeImage[]
                GetThemeImages()
            {
                return new[]
                {
                    new CompiledIconCsIdOwner.ThemedIconThemeImage("Color",
                        Load_Color),
                    new CompiledIconCsIdOwner.ThemedIconThemeImage("Gray",
                        Load_Gray),
                    new CompiledIconCsIdOwner.ThemedIconThemeImage("GrayDark",
                        Load_GrayDark)
                };
            }
        }
    }
}
