using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WebGLScreenshotTool
{
    /// <summary>Demo code for WebGLScreenshotTool.</summary>
    public class CubeDemo : MonoBehaviour
    {
        /// <summary>To rotate the scene cube.</summary>
        private void Update()
        {
            transform.Rotate(transform.right, 20 * Time.deltaTime, Space.World);
        }
    }
}
