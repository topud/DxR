﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DxR
{
    /// <summary>
    /// This is the class for point mark which enables setting of channel
    /// values which may involve calling custom scripts. The idea is that 
    /// in order to add a custom channel, the developer simply has to implement
    /// a function that takes in the "channel" name and value in string format
    /// and performs the necessary changes under the SetChannelValue function.
    /// </summary>
    public class BarMark : Mark
    {
        public BarMark() : base("bar")
        {
            
        }

        public void Start()
        {
            Vector3 origMeshSize = gameObject.GetComponent<Renderer>().bounds.extents;
            Debug.Log("Mark orig render size " + origMeshSize);
        }

        public override void SetChannelValue(string channel, string value)
        {
            switch (channel)
            {
                case "x":
                    SetX(value);
                    break;
                case "x2":
                    // TODO:
                    throw new Exception("Channel x2 not implemented.");
                    break;
                case "y":
                    SetY(value);
                    break;
                case "y2":
                    // TODO:
                    throw new Exception("Channel y2 not implemented.");
                    break;
                case "bandwidth":
                case "width":
                    SetWidth(value);
                    break;
                case "color":
                    SetColor(value);
                    break;
                default:
                    base.SetChannelValue(channel, value);
                    break;
            }
        }

        private void SetWidth(string value)
        {
            float width = float.Parse(value) * DxR.SceneObject.SIZE_UNIT_SCALE_FACTOR;

            Vector3 curScale = transform.localScale;
            GetComponent<MeshFilter>().mesh.RecalculateBounds();
            Vector3 origMeshSize = GetComponent<MeshFilter>().mesh.bounds.size;
            curScale.x = width / (origMeshSize.x);
            transform.localScale = curScale;
        }

        private void SetX(string value)
        {
            // TODO: Do this more robustly.
            float xpos = float.Parse(value) * DxR.SceneObject.SIZE_UNIT_SCALE_FACTOR;
            Debug.Log("Translating cube by " + xpos.ToString());

            float x = gameObject.transform.localPosition.x;
            transform.Translate(xpos - x, 0, 0);
        }

        private void SetY(string value)
        {
            float height = float.Parse(value) * DxR.SceneObject.SIZE_UNIT_SCALE_FACTOR;
            Vector3 curScale = transform.localScale;
            GetComponent<MeshFilter>().mesh.RecalculateBounds();
            Vector3 origMeshSize = GetComponent<MeshFilter>().mesh.bounds.size;
            curScale.y = height / (origMeshSize.y);
            transform.localScale = curScale;

            //float y = gameObject.transform.localPosition.y;
//            Vector3 center = GetComponent<MeshFilter>().mesh.bounds.center;
            transform.Translate(0, height / 2.0f, 0);
        }

        private void SetColor(string value)
        {
            Color color;
            bool colorParsed = ColorUtility.TryParseHtmlString(value, out color);
            transform.GetComponent<Renderer>().material.color = color;
        }
    }

}
