using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CustomDyes
{
	public class CustomDyeData {
		public delegate void DyeEffectDelegate(DyeEffectModifier instance, CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData);
		public static Dictionary<string, DyeParameter> modifiers;

		public static void Initialize() {
			modifiers = new Dictionary<string, DyeParameter>();
		}

		public static void Uninitialize() {
			modifiers = null;
		}

		//public static DyeModifier<Color> GetColorModifierFromTag() {

        //}

		public class DyeEffectModifier
		{
			public DyeModifier<float> opacity;
			public DyeModifier<float> saturation;
			public DyeModifier<Color> primaryColor;
			public DyeModifier<Color> secondaryColor;

			public DyeEffectDelegate GetEffect;

			public DyeEffectModifier(DyeEffectDelegate effect = null, Color primaryColor = default, Color secondaryColor = default, float opacity = 1.0f, float saturation = 1.0f) 
				: this(effect,
					  new DyeModifier<Color>(primaryColor),
					  new DyeModifier<Color>(secondaryColor),
					  new DyeModifier<float>(opacity),
					  new DyeModifier<float>(saturation))
				{ }

			public DyeEffectModifier(DyeEffectDelegate effect = null, DyeModifier<Color> primaryColor = null, DyeModifier<Color> secondaryColor = null, DyeModifier<float> opacity = null, DyeModifier<float> saturation = null) {
				this.opacity = opacity ?? new DyeModifier<float>(1.0f);
				this.saturation = saturation ?? new DyeModifier<float>(1.0f);
				this.primaryColor = primaryColor ?? new DyeModifier<Color>((Color)default);
				this.secondaryColor = secondaryColor ?? new DyeModifier<Color>((Color)default);

				this.GetEffect = effect ?? ((instance, data, shader, entity, drawData) => {
					shader.UseColor(primaryColor.GetValue(data, shader, entity, drawData));
					shader.UseSecondaryColor(secondaryColor.GetValue(data, shader, entity, drawData));
					shader.UseOpacity(opacity.GetValue(data, shader, entity, drawData));
					shader.UseSaturation(saturation.GetValue(data, shader, entity, drawData));
				});
			}
		}

		public class DyeModifier<T>
		{
			public delegate T DyeModifierDelegate(DyeModifier<T> instance, CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData);
			
			private DyeModifierDelegate _value;
			public T GetValue(CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData)
				=> _value != null ? _value.Invoke(this, data, shader, entity, drawData) : default;
			public string GetIdentifier() => "default";

			public TagCompound parameters;

			public DyeModifier(DyeModifierDelegate value = null) {
				_value = value;
			}

			public DyeModifier(T value) : this((instance, data, shader, entity, drawData) => value) { }
		}

		public abstract class DyeParameter { }

		public abstract class DyeParameter<T> : DyeParameter
        {
			public abstract T GetValue(CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData);
        }

		public class ColorParameterColor : DyeParameter<Color>
        {
			private DyeModifier<Color> _value;

			public ColorParameterColor(DyeModifier<Color> value = null) {
				_value = value ?? new DyeModifier<Color>((Color)default);
			}

			public ColorParameterColor(Color color = default)
				: this(new DyeModifier<Color>(color)) { }

			public override Color GetValue(CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData)
				=> _value.GetValue(data, shader, entity, drawData);
        }

		public class ColorParameterFloat : DyeParameter<Color>
        {
			private DyeModifier<float> _valueR;
			private DyeModifier<float> _valueG;
			private DyeModifier<float> _valueB;

			public ColorParameterFloat(DyeModifier<float> valueR = null, DyeModifier<float> valueG = null, DyeModifier<float> valueB = null) {
				_valueR = valueR ?? new DyeModifier<float>((float)default);
				_valueG = valueG ?? new DyeModifier<float>((float)default);
				_valueB = valueB ?? new DyeModifier<float>((float)default);
			}

			public ColorParameterFloat(float r = default, float g = default, float b = default) 
				: this(new DyeModifier<float>(r),
					  new DyeModifier<float>(g),
					  new DyeModifier<float>(b)) { }

			public override Color GetValue(CustomDyeData data, CustomShader shader, Entity entity, DrawData? drawData)
				=> new Color(_valueR.GetValue(data, shader, entity, drawData), 
					_valueG.GetValue(data, shader, entity, drawData), 
					_valueB.GetValue(data, shader, entity, drawData));
		}
	}

    public class ColorModifierDeserializer : TagSerializer<CustomDyeData.DyeParameter<Color>, TagCompound>
    {
        public override CustomDyeData.DyeParameter<Color> Deserialize(TagCompound tag) {
            if(tag.ContainsKey("color")) {
				return new CustomDyeData.ColorParameterColor(tag.Get<Color>("color"));
            }

			if(tag.ContainsKey("modifier")) {

            }

			return null;
        }

        public override TagCompound Serialize(CustomDyeData.DyeParameter<Color> value) {
            throw new NotImplementedException();
        }
    }
}