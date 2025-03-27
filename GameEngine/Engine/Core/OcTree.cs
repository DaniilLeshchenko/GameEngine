
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEngine.Engine.Core
{
    public class OcTree
    {
        public BoundingBox Bounds { get; set; }
        public List<RenderComponent> Objects { get; set; }

        private float Size = 20;
        public Vector3 Center = Vector3.Zero;

        private int MaxNumberOfObjects = 10;

        public List<OcTree> Nodes { get; set; }

        public OcTree(Vector3 position, float size)
        {
            Center = position;
            Size = size;

            Objects = new List<RenderComponent>();
            Nodes = new List<OcTree>();

            var minV2 = Vector3.Subtract(Center, new Vector3(size / 2, size / 2, size / 2));
            var maxV2 = Vector3.Add(Center, new Vector3(size / 2, size / 2, size / 2));

            Bounds = new BoundingBox(
                new Vector3(minV2.X, minV2.Y, minV2.Z),
                new Vector3(maxV2.X, maxV2.Y, maxV2.Z));
        }

        public void Clear()
        {
            Objects.Clear();

            foreach (var node in Nodes)
                ClearNode(node);

            Nodes.Clear();
        }

        private void ClearNode(OcTree node)
        {
            if (node == null)
                return;

            node.Clear();
            node = null;
        }

        public void SubDivide()
        {
            float subWidth = ((Bounds.Max - Bounds.Min) / 4).X;
            float subHeight = ((Bounds.Max - Bounds.Min) / 4).Y;
            subHeight = subWidth;

            //upper
            var nodeUFR = new OcTree(new Vector3(Center.X + subWidth, Center.Y - subWidth, Center.Z + subWidth), Size / 2);
            var nodeUFL = new OcTree(new Vector3(Center.X - subWidth, Center.Y - subWidth, Center.Z + subWidth), Size / 2);
            var nodeUBR = new OcTree(new Vector3(Center.X + subWidth, Center.Y - subWidth, Center.Z - subWidth), Size / 2);
            var nodeUBL = new OcTree(new Vector3(Center.X - subWidth, Center.Y - subWidth, Center.Z - subWidth), Size / 2);
            //lower
            var nodeLFR = new OcTree(new Vector3(Center.X + subWidth, Center.Y + subWidth, Center.Z + subWidth), Size / 2);
            var nodeLFL = new OcTree(new Vector3(Center.X - subWidth, Center.Y + subWidth, Center.Z + subWidth), Size / 2);
            var nodeLBR = new OcTree(new Vector3(Center.X + subWidth, Center.Y + subWidth, Center.Z - subWidth), Size / 2);
            var nodeLBL = new OcTree(new Vector3(Center.X - subWidth, Center.Y + subWidth, Center.Z - subWidth), Size / 2);

            //upper
            Nodes.Add(nodeUFR);
            Nodes.Add(nodeUFL);
            Nodes.Add(nodeUBR);
            Nodes.Add(nodeUBL);
            //lower
            Nodes.Add(nodeLFR);
            Nodes.Add(nodeLFL);
            Nodes.Add(nodeLBR);
            Nodes.Add(nodeLBL);

        }

        public void AddObject(RenderComponent newObject)
        {
            if (Nodes.Count == 0)
            {
                bool maxObjectReached = Objects.Count > MaxNumberOfObjects;

                if (maxObjectReached)
                {
                    SubDivide();

                    foreach (var obj in Objects)
                        Distrubte(obj);

                    Objects.Clear();
                }
                else
                {
                    Objects.Add(newObject);
                }
            }
            else
            {
                Distrubte(newObject);
            }
        }

        private void Distrubte(RenderComponent newObject)
        {
            var location = newObject.GameObject.Transform.Position;

            foreach (var node in Nodes)
                if (node.Bounds.Contains(location) != ContainmentType.Disjoint)
                    node.AddObject(newObject);
        }

        public void ProcessTree(BoundingFrustum frustum,  List<RenderComponent> passedObjects)
        {
            if (passedObjects == null)
                passedObjects = new List<RenderComponent>();

            var containment = frustum.Contains(Bounds);

            if (containment != ContainmentType.Disjoint)
            {
                foreach (var go in Objects)
                    passedObjects.Add(go);

                foreach (var node in Nodes)
                    node.ProcessTree(frustum,  passedObjects);
            }
        }

        public void GetAllGameObjects(ref List<RenderComponent> passedObjects)
        {
            if (passedObjects == null)
                passedObjects = new List<RenderComponent>();

            foreach (var go in Objects)
            {
                passedObjects.Add(go);
            }

            foreach (var node in Nodes)
                node.GetAllGameObjects(ref passedObjects);
        }

    }
}
