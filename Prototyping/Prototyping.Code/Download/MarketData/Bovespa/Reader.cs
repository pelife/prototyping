﻿using FlatFile.FixedLength.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using FlatFile.Core;
using FlatFile.FixedLength;
using System.Globalization;
using Prototyping.Code.Download.MarketData.Bovespa;

namespace Prototyping.Code.Download.MarketData.Bovespa
{

    public class BDIDateTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.DateTime) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            DateTime? newDate = null;
            newDate = ParseDateTime(source);
            return newDate;
        }

        public string ConvertToString(object source)
        {
            string dateFormated = string.Empty;
            DateTime? oldDate = (System.DateTime?)source;

            if (oldDate.HasValue)
                dateFormated = oldDate.Value.ToString("yyyyMMdd");

            return dateFormated;
        }

        private DateTime? ParseDateTime(
            string dateToParse,
            string[] formats = null,
            IFormatProvider provider = null,
            DateTimeStyles styles = DateTimeStyles.AssumeLocal)
        {
            string[] CUSTOM_DATE_FORMATS = new string[]
                {
                    "yyyyMMddTHHmmssZ",
                    "yyyyMMddTHHmmZ",
                    "yyyyMMddTHHmmss",
                    "yyyyMMddTHHmm",
                    "yyyyMMddHHmmss",
                    "yyyyMMddHHmm",
                    "yyyyMMdd",
                    "yyyy-MM-dd-HH-mm-ss",
                    "yyyy-MM-dd-HH-mm",
                    "yyyy-MM-dd",
                    "MM-dd-yyyy"
                };

            if (formats == null)
            {
                formats = CUSTOM_DATE_FORMATS;
            }

            DateTime validDate;

            foreach (var format in formats)
            {
                if (format.EndsWith("Z"))
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider,
                             DateTimeStyles.AssumeUniversal,
                             out validDate))
                    {
                        return validDate;
                    }
                }
                else
                {
                    if (DateTime.TryParseExact(dateToParse, format,
                             provider, styles, out validDate))
                    {
                        return validDate;
                    }
                }
            }

            return null;
        }
    }

    public class BDIStringTypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.String) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            string newDate = string.Empty;
            if (!string.IsNullOrEmpty(source))
                newDate = source.Trim();
            return newDate;
        }

        public string ConvertToString(object source)
        {
            string dateFormated = string.Empty;
            string oldDate = (string)source;

            if (!string.IsNullOrEmpty(oldDate))
                dateFormated = oldDate.Trim();

            return dateFormated;
        }
    }

    public class BDIDouble100TypeConverter : ITypeConverter
    {
        public bool CanConvertFrom(Type type)
        {
            bool ret = typeof(String) == type;
            return ret;
        }

        public bool CanConvertTo(Type type)
        {
            bool ret = typeof(System.Double) == type;
            return ret;
        }

        public object ConvertFromString(string source)
        {
            double? newDouble = null;
            double doubleAux = 0.0;
            if (!string.IsNullOrEmpty(source))
                if (double.TryParse(source, out doubleAux))
                {
                    doubleAux = doubleAux / 100;
                    newDouble = doubleAux;
                }
            return newDouble;
        }

        public string ConvertToString(object source)
        {
            string doubleFormated = string.Empty;
            double? oldDouble = (double?)source;
            if (oldDouble.HasValue)
                doubleFormated = oldDouble.Value.ToString("N2");

            return doubleFormated;
        }
    }

    public class BDIHeader
    {
        public int Tipo { get; set; }
        public string NomeArquivo { get; set; }
        public string Origem { get; set; }
        public int Destino { get; set; }
        public DateTime DataGeracao { get; set; }
        public DateTime DataPregao { get; set; }
        public string HoraMinuto { get; set; }
    }
    public class BDIIndice
    {
        public int Tipo { get; set; }
        public int Identificador { get; set; }
        public string Nome { get; set; }
        public double Abertura { get; set; }
        public double Minimo { get; set; }
        public double Maximo { get; set; }
        public double Media { get; set; }
        public double IndiceLiquidacao { get; set; }
        public double MaximoAno { get; set; }
        public DateTime DataMaximoAno { get; set; }
        public double MinimoAno { get; set; }
        public DateTime DataMinimoAno { get; set; }
        public double Fechamento { get; set; }
        public char SinalEvolucaoPercentualFechamento { get; set; }
        public double EvolucaoPercentualFechamento { get; set; }
        public char SinalEvolucaoPercentualOntem { get; set; }
        public double EvolucaoPercentualOntem { get; set; }
        public char SinalEvolucaoPercentualSemana { get; set; }
        public double EvolucaoPercentualSemana { get; set; }
        public char SinalEvolucaoPercentualMesAtual { get; set; }
        public double EvolucaoPercentualMesAtual { get; set; }
        public char SinalEvolucaoPercentualUmMes { get; set; }
        public double EvolucaoPercentualUmMes { get; set; }
        public char SinalEvolucaoPercentualAnoAtual { get; set; }
        public double EvolucaoPercentualAnoAtual { get; set; }
        public char SinalEvolucaoPercentualUmAno { get; set; }
        public double EvolucaoPercentualUmAno { get; set; }
        public double NumeroAcoesAlta { get; set; }
        public double NumeroAcoesBaixa { get; set; }
        public double NumeroAcoesEstaveis { get; set; }
        public double NumeroTotalAcoes { get; set; }
        public double NumeroNegociosAcoesIndice { get; set; }
        public double NumeroNegociosTitulosAcoesIndice { get; set; }
        public double VolumeNegociosAcoesIndice { get; set; }
        public double IndiceMediaPonderada { get; set; }
    }
    public class BDINegociosPapel
    {
        public int Tipo { get; set; }
        public string CodigoBDI { get; set; }
        public string DesricaoBDI { get; set; }
        public string NomeResumido { get; set; }
        public string Especificacao { get; set; }
        public string Caracteristica { get; set; }
        public string CodigoNegociacao { get; set; }
        public int CodigoTipoMercado { get; set; }
        public string DescricaoTipoMercado { get; set; }
        public int PrazoDiasTermo { get; set; }
        public double Abertura { get; set; }
        public double Maximo { get; set; }
        public double Minimo { get; set; }
        public double Media { get; set; }
        public double Ultimo { get; set; }
        public char SinalOscilacaoPrecoPregaoAnterior { get; set; }
        public double OscilacaoPrecoPregaoAnterior { get; set; }
        public double MelhorOfertaCompra { get; set; }
        public double MelhorOfertaVenda { get; set; }
        public double NumeroNegocios { get; set; }
        public double QuantidadeTitulosNegociados { get; set; }
        public double VolumeTotalTitulosNeogicos { get; set; }
        public double PrecoExercicioOpcoesValorContratoTermo { get; set; }
        public DateTime? DataVencimento { get; set; }
        public double IndicadorCorrecaoExercicioValorContratoTermoFuturo { get; set; }
        public string NomeIndicadorCorrecaoExercicioValorContratoTermoFuturo { get; set; }
        public double FatorCotacao { get; set; }
        public double PrecoExercicioEmPontosOpcoesReferenciadasDolar { get; set; }
        public string CodigoISIN { get; set; }
        public double NumeroDistribuicaoPapel { get; set; }
        public double CodigoEstiloExercicioOpcao { get; set; }
        public string DescricaoEstiloExercicioOpcao { get; set; }
        public double IndicadorCorrecaoExercicioValorContratoTermoFuturo2 { get; set; }
        public double OscilacaoPrecoPregaoAnterior2 { get; set; }
    }
    public class BDINegociosBDI { }
    public class BDIMaiorOscilacaoVista
    {

        public int Tipo { get; set; }
        public char IndicadorAltaBaixa { get; set; } // a = alta - b = baixa
        public string NomeResumido { get; set; }
        public string Especificacao { get; set; }
        public double Ultimo { get; set; }
        public double NumeroNegocios { get; set; }
        public double OscilacaoPrecoPregaoAnterior { get; set; }
        public double OscilacaoPreco { get; set; }
        public string CodigoNegociacao { get; set; }

    }
    public class BDIMaiorOscilacaoAcoesIBOV : BDIMaiorOscilacaoVista { }
    public class BDIMaisNegociadasMercadoVista
    {
        public int Tipo { get; set; }
        public string NomeResumido { get; set; }
        public string Especificacao { get; set; }
        public double QuantidadeTitulosNegociados { get; set; }
        public double VolumeTotalTitulosNeogicos { get; set; }
        public string CodigoNegociacao { get; set; }
    }
    public class BDIMaisNegociadasMercado
    {
        public int Tipo { get; set; }
        public int CodigoTipoMercado { get; set; }
        public string DescricaoTipoMercado { get; set; }
        public string NomeResumido { get; set; }
        public string Especificacao { get; set; }
        public double IndicadorCorrecaoExercicioValorContratoTermoFuturo { get; set; }
        public string NomeIndicadorCorrecaoExercicioValorContratoTermoFuturo { get; set; }
        public double PrecoExercicioOpcoesValorContratoTermo { get; set; }
        public DateTime? DataVencimento { get; set; }
        public int PrazoDiasTermo { get; set; }
        public double QuantidadeTitulosNegociados { get; set; }
        public double VolumeTotalTitulosNeogicos { get; set; }
        public double ParticipacaoVolumePapelVolumeMercado { get; set; }
        public string CodigoNegociacao { get; set; }
        public int IndicadorCorrecaoPrecoAtivo { get; set; }
    }
    public class BDIResumoDiarioIOPV
    {
        public int Tipo { get; set; }
        public int Identificador { get; set; }
        public string Sigla { get; set; }
        public string NomeResumido { get; set; }
        public string Nome { get; set; }
        public double Abertura { get; set; }
        public double Maximo { get; set; }
        public double Minimo { get; set; }
        public double Media { get; set; }
        public double Fechamento { get; set; }
        public char SinalEvolucaoPercentualFechamento { get; set; }
        public double EvolucaoPercentualFechamento { get; set; }
    }
    public class BDIBDRNaoPatrocinado
    {
        public int Tipo { get; set; }
        public string CodigoNegociacao { get; set; }
        public string NomeResumido { get; set; }
        public string Especificacao { get; set; }
        public double ValorReferencia { get; set; }
    }
    public class BDITrailer
    {
        public int Tipo { get; set; }
        public string NomeArquivo { get; set; }
        public string Origem { get; set; }
        public int Destino { get; set; }
        public DateTime DataGeracao { get; set; }
        public int TotalRegistros { get; set; }
    }

    public sealed class BDIHeaderLayout : FixedLayout<BDIHeader>
    {
        public BDIHeaderLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.NomeArquivo, c => c.WithLength(8))
                .WithMember(x => x.Origem, c => c.WithLength(8))
                .WithMember(x => x.Destino, c => c.WithLength(4))
                .WithMember(x => x.DataGeracao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.DataPregao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.HoraMinuto, c => c.WithLength(4));
        }
    }

    public sealed class BDIIndiceLayout : FixedLayout<BDIIndice>
    {
        public BDIIndiceLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.Identificador, c => c.WithLength(2))
                .WithMember(x => x.Nome, c => c.WithLength(30).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Abertura, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.Minimo, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.Maximo, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.Media, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.IndiceLiquidacao, c => c.WithLength(6))
                .WithMember(x => x.MaximoAno, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.DataMaximoAno, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.MinimoAno, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.DataMinimoAno, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.Fechamento, c => c.WithLength(6).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualFechamento, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualFechamento, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualOntem, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualOntem, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualFechamento, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualFechamento, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualSemana, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualSemana, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualMesAtual, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualMesAtual, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualUmMes, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualUmMes, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualAnoAtual, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualAnoAtual, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.SinalEvolucaoPercentualUmAno, c => c.WithLength(1))
                .WithMember(x => x.EvolucaoPercentualUmAno, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.NumeroAcoesAlta, c => c.WithLength(3))
                .WithMember(x => x.NumeroAcoesBaixa, c => c.WithLength(3))
                .WithMember(x => x.NumeroAcoesEstaveis, c => c.WithLength(3))
                .WithMember(x => x.NumeroTotalAcoes, c => c.WithLength(3))
                .WithMember(x => x.NumeroNegociosAcoesIndice, c => c.WithLength(6))
                .WithMember(x => x.NumeroNegociosTitulosAcoesIndice, c => c.WithLength(15))
                .WithMember(x => x.VolumeNegociosAcoesIndice, c => c.WithLength(17).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.IndiceMediaPonderada, c => c.WithLength(6));
        }
    }

    public sealed class BDINegociosPapelLayout : FixedLayout<BDINegociosPapel>
    {
        public BDINegociosPapelLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
             .WithMember(x => x.CodigoBDI, c => c.WithLength(2).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.DesricaoBDI, c => c.WithLength(30).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.Caracteristica, c => c.WithLength(1).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.CodigoTipoMercado, c => c.WithLength(3))
             .WithMember(x => x.DescricaoTipoMercado, c => c.WithLength(15).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.PrazoDiasTermo, c => c.WithLength(3))
             .WithMember(x => x.Abertura, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.Maximo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.Minimo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.Media, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.Ultimo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.SinalOscilacaoPrecoPregaoAnterior, c => c.WithLength(1))
             .WithMember(x => x.OscilacaoPrecoPregaoAnterior, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.MelhorOfertaCompra, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.MelhorOfertaVenda, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.NumeroNegocios, c => c.WithLength(5))
             .WithMember(x => x.QuantidadeTitulosNegociados, c => c.WithLength(15))
             .WithMember(x => x.VolumeTotalTitulosNeogicos, c => c.WithLength(17).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.PrecoExercicioOpcoesValorContratoTermo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.DataVencimento, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
             .WithMember(x => x.IndicadorCorrecaoExercicioValorContratoTermoFuturo, c => c.WithLength(1))
             .WithMember(x => x.NomeIndicadorCorrecaoExercicioValorContratoTermoFuturo, c => c.WithLength(15).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.FatorCotacao, c => c.WithLength(7))
             .WithMember(x => x.PrecoExercicioEmPontosOpcoesReferenciadasDolar, c => c.WithLength(13).WithTypeConverter<BDIDouble100TypeConverter>())
             .WithMember(x => x.CodigoISIN, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.NumeroDistribuicaoPapel, c => c.WithLength(3))
             .WithMember(x => x.CodigoEstiloExercicioOpcao, c => c.WithLength(1).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.DescricaoEstiloExercicioOpcao, c => c.WithLength(15).WithTypeConverter<BDIStringTypeConverter>())
             .WithMember(x => x.IndicadorCorrecaoExercicioValorContratoTermoFuturo2, c => c.WithLength(3))
             .WithMember(x => x.OscilacaoPrecoPregaoAnterior2, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>());
        }
    }

    public sealed class BDITrailerLayout : FixedLayout<BDITrailer>
    {
        public BDITrailerLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.NomeArquivo, c => c.WithLength(8))
                .WithMember(x => x.Origem, c => c.WithLength(8))
                .WithMember(x => x.Destino, c => c.WithLength(4))
                .WithMember(x => x.DataGeracao, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.TotalRegistros, c => c.WithLength(9));
        }
    }

    public sealed class BDIMaioresOscilacoesVistaLayout : FixedLayout<BDIMaiorOscilacaoVista>
    {
        public BDIMaioresOscilacoesVistaLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.IndicadorAltaBaixa, c => c.WithLength(1)) // a = alta - b = baixa
                .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Ultimo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.NumeroNegocios, c => c.WithLength(5))
                .WithMember(x => x.OscilacaoPrecoPregaoAnterior, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.OscilacaoPreco, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>());
        }
    }

    public sealed class BDIMaioresOscilacoesIBOVLayout : FixedLayout<BDIMaiorOscilacaoAcoesIBOV>
    {
        public BDIMaioresOscilacoesIBOVLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.IndicadorAltaBaixa, c => c.WithLength(1)) // a = alta - b = baixa
                .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Ultimo, c => c.WithLength(11).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.NumeroNegocios, c => c.WithLength(5))
                .WithMember(x => x.OscilacaoPrecoPregaoAnterior, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.OscilacaoPreco, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>());
        }
    }
    public sealed class BDIMaisNegociadasMercadoVistaLayout : FixedLayout<BDIMaisNegociadasMercadoVista>
    {

        public BDIMaisNegociadasMercadoVistaLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.QuantidadeTitulosNegociados, c => c.WithLength(15))
                .WithMember(x => x.VolumeTotalTitulosNeogicos, c => c.WithLength(17).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>());
        }
    }

    public sealed class BDIMaisNegociadasMercadoLayout : FixedLayout<BDIMaisNegociadasMercado>
    {
        public BDIMaisNegociadasMercadoLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
                .WithMember(x => x.CodigoTipoMercado, c => c.WithLength(3))
                .WithMember(x => x.DescricaoTipoMercado, c => c.WithLength(20).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.Especificacao, c => c.WithLength(10).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.IndicadorCorrecaoExercicioValorContratoTermoFuturo, c => c.WithLength(2))
                .WithMember(x => x.NomeIndicadorCorrecaoExercicioValorContratoTermoFuturo, c => c.WithLength(15).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.PrecoExercicioOpcoesValorContratoTermo, c => c.WithLength(11))
                .WithMember(x => x.DataVencimento, c => c.WithLength(8).WithTypeConverter<BDIDateTypeConverter>())
                .WithMember(x => x.PrazoDiasTermo, c => c.WithLength(3))
                .WithMember(x => x.QuantidadeTitulosNegociados, c => c.WithLength(15))
                .WithMember(x => x.VolumeTotalTitulosNeogicos, c => c.WithLength(17).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.ParticipacaoVolumePapelVolumeMercado, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>())
                .WithMember(x => x.CodigoNegociacao, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
                .WithMember(x => x.IndicadorCorrecaoPrecoAtivo, c => c.WithLength(3));
        }
    }

    public sealed class BDIResumoDiarioIOPVLayout : FixedLayout<BDIResumoDiarioIOPV>
    {
        public BDIResumoDiarioIOPVLayout()
        {
            this.WithMember(x => x.Tipo, c => c.WithLength(2))
            .WithMember(x => x.Identificador, c => c.WithLength(2))
            .WithMember(x => x.Sigla, c => c.WithLength(4).WithTypeConverter<BDIStringTypeConverter>())
            .WithMember(x => x.NomeResumido, c => c.WithLength(12).WithTypeConverter<BDIStringTypeConverter>())
            .WithMember(x => x.Nome, c => c.WithLength(30).WithTypeConverter<BDIStringTypeConverter>())
            .WithMember(x => x.Abertura, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>())
            .WithMember(x => x.Maximo, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>())
            .WithMember(x => x.Minimo, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>())
            .WithMember(x => x.Media, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>())
            .WithMember(x => x.Fechamento, c => c.WithLength(7).WithTypeConverter<BDIDouble100TypeConverter>())
            .WithMember(x => x.SinalEvolucaoPercentualFechamento, c => c.WithLength(1))
            .WithMember(x => x.EvolucaoPercentualFechamento, c => c.WithLength(5).WithTypeConverter<BDIDouble100TypeConverter>());
        }
    }


    public class ReaderBDI
    {
        const string enderecoArquivoBDI = @"G:\shared\bdi0713\BDIN";

        public void Read()
        {
            //
            var factory = new FixedLengthFileEngineFactory();
            using (var stream = new FileInfo(enderecoArquivoBDI).Open(FileMode.Open, FileAccess.Read, FileShare.Read))
            using (var streamReader = new StreamReader(stream, Encoding.ASCII))
            {
                // If using attribute mapping, pass an array of record types
                // rather than layout instances
                var layouts = new ILayoutDescriptor<IFixedFieldSettingsContainer>[]
                {
                    new BDIHeaderLayout()
                    ,new BDIIndiceLayout()
                    ,new BDINegociosPapelLayout()
                    ,new BDITrailerLayout()
                    ,new BDIMaioresOscilacoesVistaLayout()
                    ,new BDIMaioresOscilacoesIBOVLayout()
                    ,new BDIMaisNegociadasMercadoVistaLayout()
                    ,new BDIMaisNegociadasMercadoLayout()
                    ,new BDIResumoDiarioIOPVLayout()

                };

                var flatFile = factory.GetEngine(layouts,
                    line =>
                    {
                        // For each line, return the proper record type.
                        // The mapping for this line will be loaded based on that type.
                        // In this simple example, the first character determines the
                        // record type.
                        if (String.IsNullOrEmpty(line) || line.Length < 1) return null;
                        switch (line.Substring(0, 2))
                        {
                            case "00":
                                return typeof(BDIHeader);
                            case "01":
                                return typeof(BDIIndice);
                            case "02":
                                return typeof(BDINegociosPapel);
                            case "04":
                                return typeof(BDIMaiorOscilacaoVista);
                            case "05":
                                return typeof(BDIMaiorOscilacaoAcoesIBOV);
                            case "06":
                                return typeof(BDIMaisNegociadasMercadoVista);
                            case "07":
                                return typeof(BDIMaisNegociadasMercado);
                            case "08":
                                return typeof(BDIResumoDiarioIOPV);
                            case "99":
                                return typeof(BDITrailer);
                        }
                        return null;
                    });

                
                flatFile.Read(streamReader.)

                var header = flatFile.GetRecords<BDIHeader>().FirstOrDefault();
                var indices = flatFile.GetRecords<BDIIndice>().ToList();
                var negocios = flatFile.GetRecords<BDINegociosPapel>().ToList();
                var oscilaVista = flatFile.GetRecords<BDIMaiorOscilacaoVista>().ToList();
                var oscilaIbov = flatFile.GetRecords<BDIMaiorOscilacaoAcoesIBOV>().ToList();
                var maisNegociadasVista = flatFile.GetRecords<BDIMaisNegociadasMercadoVista>().ToList();
                var maisNegociadasMerca = flatFile.GetRecords<BDIMaisNegociadasMercado>().ToList();
                var IOPV = flatFile.GetRecords<BDIResumoDiarioIOPV>().ToList();
                var trailer = flatFile.GetRecords<BDITrailer>().FirstOrDefault();
            }
        }
    }


}