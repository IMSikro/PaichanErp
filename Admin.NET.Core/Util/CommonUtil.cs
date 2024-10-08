﻿// 麻省理工学院许可证
//
// 版权所有 (c) 2021-2023 zuohuaijun，大名科技（天津）有限公司  联系电话/微信：18020030720  QQ：515096995
//
// 特此免费授予获得本软件的任何人以处理本软件的权利，但须遵守以下条件：在所有副本或重要部分的软件中必须包括上述版权声明和本许可声明。
//
// 软件按“原样”提供，不提供任何形式的明示或暗示的保证，包括但不限于对适销性、适用性和非侵权的保证。
// 在任何情况下，作者或版权持有人均不对任何索赔、损害或其他责任负责，无论是因合同、侵权或其他方式引起的，与软件或其使用或其他交易有关。

using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Admin.NET.Core;

/// <summary>
/// 通用工具类
/// </summary>
public static class CommonUtil
{
    /// <summary>
    /// 生成百分数
    /// </summary>
    /// <param name="PassCount"></param>
    /// <param name="allCount"></param>
    /// <returns></returns>
    public static string ExecPercent(decimal PassCount, decimal allCount)
    {
        string res = "";
        if (allCount > 0)
        {
            var value = (double)Math.Round(PassCount / allCount * 100, 1);
            if (value < 0)
                res = Math.Round(value + 5 / Math.Pow(10, 0 + 1), 0, MidpointRounding.AwayFromZero).ToString();
            else
                res = Math.Round(value, 0, MidpointRounding.AwayFromZero).ToString();
        }
        if (res == "") res = "0";
        return res + "%";
    }

    /// <summary>
    /// 获取服务地址
    /// </summary>
    /// <returns></returns>
    public static string GetLocalhost()
    {
        string result = $"{App.HttpContext.Request.Scheme}://{App.HttpContext.Request.Host.Value}";
        // 代理模式：获取真正的本机地址
        // X-Original-Host=原始请求
        // X-Forwarded-Server=从哪里转发过来
        if (App.HttpContext.Request.Headers.ContainsKey("X-Original-Host"))
            result = $"{App.HttpContext.Request.Scheme}://{App.HttpContext.Request.Headers["X-Original-Host"]}";
        return result;
    }

    /// <summary>
    /// 对象序列化XML
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static string SerializeObjectToXml<T>(T obj)
    {
        if (obj == null) return string.Empty;

        var xs = new XmlSerializer(obj.GetType());
        var stream = new MemoryStream();
        var setting = new XmlWriterSettings
        {
            Encoding = new UTF8Encoding(false), // 不包含BOM
            Indent = true // 设置格式化缩进
        };
        using (var writer = XmlWriter.Create(stream, setting))
        {
            var ns = new XmlSerializerNamespaces();
            ns.Add("", ""); // 去除默认命名空间
            xs.Serialize(writer, obj, ns);
        }
        return Encoding.UTF8.GetString(stream.ToArray());
    }

    /// <summary>
    /// 字符串转XML格式
    /// </summary>
    /// <param name="xmlStr"></param>
    /// <returns></returns>
    public static XElement SerializeStringToXml(string xmlStr)
    {
        try
        {
            return XElement.Parse(xmlStr);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 导出模板Excel
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="fileDto"></param>
    /// <returns></returns>
    public static async Task<IActionResult> ExportExcelTemplate(string fileName, dynamic fileDto)
    {
        fileName = $"{fileName}_{DateTime.Now.ToString("yyyyMMddHHmmss")}.xlsx";

        IImporter importer = new ExcelImporter();
        MethodInfo generateTemplateMethod = importer.GetType().GetMethod("GenerateTemplate");
        MethodInfo closedGenerateTemplateMethod = generateTemplateMethod.MakeGenericMethod(fileDto.GetType());
        var res = await (Task<dynamic>)closedGenerateTemplateMethod.Invoke(importer, new object[] { Path.Combine(App.WebHostEnvironment.WebRootPath, fileName) });

        return new FileStreamResult(new FileStream(res.FileName, FileMode.Open), "application/octet-stream") { FileDownloadName = fileName };
    }

    /// <summary>
    /// 导入数据Excel
    /// </summary>
    /// <param name="file"></param>
    /// <param name="dataDto"></param>
    /// <returns></returns>
    public static async Task<dynamic> ImportExcelData([Required] IFormFile file, dynamic dataDto)
    {
        var newFile = await App.GetRequiredService<SysFileService>().UploadFile(file, "");
        var filePath = Path.Combine(App.WebHostEnvironment.WebRootPath, newFile.FilePath, newFile.Name);

        IImporter importer = new ExcelImporter();
        MethodInfo importMethod = importer.GetType().GetMethod("Import");
        MethodInfo closedImportMethod = importMethod.MakeGenericMethod(dataDto.GetType());
        var res = await (Task<dynamic>)closedImportMethod.Invoke(importer, new object[] { filePath });
        if (res == null || res.Exception != null)
            throw Oops.Oh("导入异常:" + res.Exception);

        return res.Data;
    }

    public static string LabToRgb(this string lab)
    {
        var labSplit = lab.Split([',', '/', '*']);
        var L = Convert.ToDouble(labSplit[0].Trim());
        var A = Convert.ToDouble(labSplit[1].Trim());
        var B = Convert.ToDouble(labSplit[2].Trim());

        var y = (L * 1 + 16) / 116D;
        var x = A / 500D + y;
        var Z = y - B / 200D;

        var modelValue = 0.008856;
        if (Math.Pow(y, 3) > modelValue)
            y = Math.Pow(y, 3);
        else
            y = (y - 16 / 116D) / 7.787;

        if (Math.Pow(x, 3) > modelValue)
            x = Math.Pow(x, 3);
        else
            x = (x - 16 / 116D) / 7.787;

        if (Math.Pow(Z, 3) > modelValue)
            Z = Math.Pow(Z, 3);
        else
            Z = (Z - 16 / 116D) / 7.787;

        x = x * 0.95047;
        y = y * 1.0;
        Z = Z * 1.08883;

        var r = x * 3.2406 + y * -1.5372 + Z * -0.4986;
        var g = x * -0.9689 + y * 1.8758 + Z * 0.0415;
        var b = x * 0.0557 + y * -0.204 + Z * 1.057;

        if (r > 0.0031308)
            r = 1.055 * Math.Pow(r, (1 / 2.4)) - 0.055;
        else
            r = 12.92 * r;

        if (g > 0.0031308)
            g = 1.055 * Math.Pow(g, (1 / 2.4)) - 0.055;
        else
            g = 12.92 * g;

        if (b > 0.0031308)
            b = 1.055 * Math.Pow(b, (1 / 2.4)) - 0.055;
        else
            b = 12.92 * b;

        r = Math.Round(r * 255);
        g = Math.Round(g * 255);
        b = Math.Round(b * 255);

        if (r < 0) r = 0;
        if (r > 255) r = 255;
        if (g < 0) g = 0;
        if (g > 255) g = 255;
        if (b < 0) b = 0;
        if (b > 255) b = 255;

        return string.Join(',', new[] { r, g, b });
    }
}