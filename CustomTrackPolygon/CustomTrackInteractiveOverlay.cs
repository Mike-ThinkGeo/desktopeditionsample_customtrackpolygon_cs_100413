using ThinkGeo.MapSuite.Core;
using ThinkGeo.MapSuite.DesktopEdition;

namespace CustomTrackPolygon
{
    public class CustomTrackInteractiveOverlay : TrackInteractiveOverlay
    {
        const int minVertexNum = 2;

        public CustomTrackInteractiveOverlay()
            : base()
        {
            this.TrackMode = TrackMode.Polygon;
        }

        protected override InteractiveResult MouseDownCore(InteractionArguments interactionArguments)
        {
            System.Diagnostics.Debug.WriteLine(this.Vertices.Count);

            if (interactionArguments.MouseButton != MapMouseButton.Right)
            {
                return base.MouseDownCore(interactionArguments);
            }
            else
            {
                if (this.Vertices.Count >= 5)
                {
                    RemoveLastVertexAdded();
                    MouseDownCount = MouseDownCount - 1;
                }

                return new InteractiveResult();
            }
        }
        private void RemoveLastVertexAdded()
        {
            if (this.Vertices.Count > minVertexNum)
            {
                this.Lock.EnterWriteLock();
                try
                {
                    Vertex lastVertex = this.Vertices[this.Vertices.Count - 3];
                    this.Vertices.Remove(lastVertex);
                }
                finally
                {
                    this.Lock.ExitWriteLock();
                }
            }
        }

              
    }
}
