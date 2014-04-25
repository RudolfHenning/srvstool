"C:\Program Files (x86)\WiX Toolset v3.8\bin\candle.exe" -out ServicesMon3x64.winobj Productx64.wxs
"C:\Program Files (x86)\WiX Toolset v3.8\bin\light.exe" -out ServicesMon3x64.msi ServicesMon3x64.winobj -ext WixUIExtension -ext WixUtilExtension -ext WixNetfxExtension
"C:\Program Files (x86)\WiX Toolset v3.8\bin\candle.exe" -out ServicesMon3.winobj Product.wxs
"C:\Program Files (x86)\WiX Toolset v3.8\bin\light.exe" -out ServicesMon3.msi ServicesMon3.winobj -ext WixUIExtension -ext WixUtilExtension -ext WixNetfxExtension
pause