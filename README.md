# MechSprayPaint

**v1.1.0.1 and higher requires IRBTModUtils**
**v1.1.0.0 and higher requires modtek v3 or higher**

Enables specified effects with EffectTriggerType OnHit to change the paintjob of the unit hit. You can have a paintgun actually paint mechs.

The following is a list (in no particular order) of all the "color swatches" available. These are the same as the colors you can choose in company customization, but they are not in the order they appear in company customization sooo... good luck I guess.

| | | | | |
|---|---|---|---|---|
|Blue_01	|BrightPurple_01	|Green_04	|Metallic_Red	|Purple_05|
|Blue_02	|BrightPurple_02	|Green_05	|Metallic_Silver	|Red_01|
|Blue_03	|BrightRed_01	|Greyscale_01	|Orange_01	|Red_02|
|Blue_04	|BrightRed_02	|Greyscale_02	|Orange_02	|Red_03|
|Blue_05	|BrightWhite_01	|Greyscale_03	|Orange_03	|Red_04|
|BrightBlue_01	|BrightWhite_02	|Greyscale_04	|Orange_04	|Red_05|
|BrightBlue_02	|BrightYellow_01	|Greyscale_05	|Orange_05	|Yellow_01|
|BrightGreen_01	|BrightYellow_02	|Metallic_Blue	|Purple_01	|Yellow_02|
|BrightGreen_02	|Green_01	|Metallic_Chrome	|Purple_02	|Yellow_03|
|BrightOrange_01	|Green_02	|Metallic_Copper	|Purple_03	|Yellow_04|
|BrightOrange_02	|Green_03	|Metallic_Gold	|Purple_04	|Yellow_05|


mod.json settings follow:
```
"Settings": {
		"paintTypes": [
			{
				"effectID": "Effect_PaintJobber",
				"RandomColors": true,
				"primaryMechColorID": "",
				"secondaryMechColorID": "",
				"tertiaryMechColorID": ""
			},
			{
				"effectID": "Effect_PaintJobber_MardiGras",
				"RandomColors": false,
				"primaryMechColorID": "Metallic_Gold",
				"secondaryMechColorID": "BrightPurple_01",
				"tertiaryMechColorID": "BrightGreen_02"
			}
		]
	},
```

only one setting, `paintTypes`, which defines the new paints to be applied by a given effect.

  `effectID` - the effect ID which will cause paint to change. can be any effect with EffectTriggerType.OnHit. MSPaint creates a dummy statistic you may use in lieu of an existing statistic effec (example effect at bottom of this document. dummy statistic is `MSPainted`.
  
  `RandomColors` - bool. if true, new colors will be random, ignoring `primaryMechColorID`, `secondaryMechColorID`, and `tertiaryMechColorID`.
  
  `primaryMechColorID`, `secondaryMechColorID`, and `tertiaryMechColorID` - strings. Color ID (from table above) of colors to use in new paint.
  
  ```
{
            "durationData": {},
            "targetingData": {
                "effectTriggerType": "OnHit",
                "triggerLimit": 0,
                "extendDurationOnTrigger": 0,
                "specialRules": "NotSet",
                "effectTargetType": "NotSet",
                "range": 0,
                "forcePathRebuild": false,
                "forceVisRebuild": false,
                "showInTargetPreview": false,
                "showInStatusPanel": false
            },
            "effectType": "StatisticEffect",
            "Description": {
                "Id": "Effect_PaintJobber",
                "Name": "HAHAH",
                "Details": "mech change color look dumb",
                "Icon": "uixSvgIcon_status_sensorsImpaired"
            },
            "nature": "Debuff",
            "statisticData": {
                "statName": "MSPainted",
                "operation": "Set",
                "modValue": "true",
                "modType": "System.Boolean"
            }
        }
```

