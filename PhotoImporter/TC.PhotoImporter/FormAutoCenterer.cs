using System;
using System.Drawing;
using System.Windows.Forms;

namespace TC.PhotoImporter
{
    internal sealed class FormAutoCenterer : IDisposable
    {
        private readonly Form _form;
        private Size _previousSize;

        internal FormAutoCenterer(Form form)
        {
            _form = form;
            _previousSize = form.Size;
            form.Resize += OnFormResize;
        }

        public void Dispose()
        {
            _form.Resize -= OnFormResize;
        }

        private void OnFormResize(object sender, EventArgs e)
        {
            Rectangle bounds = _form.Bounds;

            _form.Location = new Point(
                x: bounds.X + (_previousSize.Width - bounds.Width) / 2,
                y: bounds.Y + (_previousSize.Height - bounds.Height) / 2);

            _previousSize = bounds.Size;
        }
    }
}
