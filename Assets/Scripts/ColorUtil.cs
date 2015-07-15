using UnityEngine;
using System.Collections;

public class ColorUtil {

	private static float lastH = 0f;

	public static Color32 getRandomColor () {
		//float goldenRatio_Conjugate = 0.618033988749895f;   
		float r = Random.Range(0.2f, 0.8f);
		lastH += r;
		lastH %= 1;     
		return hsvToRgb(lastH , Random.Range(0.8f,1.0f) ,1.0f );    
	}
	
	// h  0 -> 1
	// s  0 -> 1
	// v  0 -> 1
	
	public static Color32 hsvToRgb (float h, float s, float v) {
		int H = (int)(h * 6);
		float f = h * 6 - H;
		float p = v * (1 - s);
		float q = v * (1 - f * s);
		float t = v * (1 - (1 - f) * s);
		
		float r=0;
		float g=0;
		float b=0;
		switch (H) {
		case 0:
			r = v;
			g = t;
			b = p;
			break;  
		case 1:
			r = q;
			g = v;
			b = p;
			break;
		case 2:
			r = p;
			g = v;
			b = t;
			break;
		case 3:
			r = p;
			g = q;
			b = v;
			break;
		case 4:
			r = t;
			g = p;
			b = v;
			break;
		case 5:
			r = v;
			g = p;
			b = q;
			break;
		}
		return new UnityEngine.Color32((byte)(255*r),(byte)(255*g),(byte)(255*b),255);
	}

    public static string ConvertRGBtoHEX(Color32 rgb) {
        return rgb.r.ToString("X2") + rgb.g.ToString("X2") + rgb.b.ToString("X2");
    }

    public static Color32 ConvertHEXtoRGB(string hex) {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }
}
