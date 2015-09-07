namespace SPALM.SPSF.Library
{
    using System.Drawing;
    using System.Windows.Forms;
    using SPALM.SPSF.Library.Editors;

    internal class ImageListProvider
  {
    public static ImageList GetIcons()
    {
      ImageList list = new ImageList();
      list.ColorDepth = ColorDepth.Depth32Bit;
      list.ImageSize = new Size(16, 16);

      list.Images.Add("Feature", ResourceIcons.Feature);
      list.Images.Add("ContentType", ResourceIcons.icons_contenttype);

      return list;
    }
  }
}
