using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WireBankAPI.Models;


namespace WireBankAPI.API
{
    public class PayAPIController : ApiController
    {
        WireBankDBModel db = new WireBankDBModel();
        public string Get(string merchandID, string merchandPassword, decimal price,
            string CardNumber, string Cvv, string month, string year)
        {
            if (!string.IsNullOrEmpty(merchandID))
            {
                if (merchandID.Length == 6)
                {
                    if (!string.IsNullOrEmpty(merchandPassword))
                    {
                        if (merchandPassword.Length == 4)
                        {
                            PosMusterileri pm = db.PosMusterileri.FirstOrDefault(x => x.MusteriNumarasi == merchandID && x.MusteriSifre == merchandPassword);
                            if (pm != null)
                            {
                                if (Convert.ToBoolean(pm.Durum))
                                {
                                    if (!string.IsNullOrEmpty(CardNumber))
                                    {
                                        if (!string.IsNullOrEmpty(Cvv))
                                        {
                                            if (!string.IsNullOrEmpty(month))
                                            {
                                                if (!string.IsNullOrEmpty(year))
                                                {
                                                    Kartlar k = db.Kartlar.FirstOrDefault(x => x.KartNo == CardNumber);
                                                    if (k != null)
                                                    {
                                                        string Day = DateTime.Now.Day.ToString();
                                                        string Check = Day + "/" + month + "/" + year;
                                                        DateTime cartDate = Convert.ToDateTime(Check);
                                                        if (cartDate >= DateTime.Now)
                                                        {
                                                            if (k.CVV == Cvv)
                                                            {
                                                                if (price > 0)
                                                                {
                                                                    Musteriler m = db.Musteriler.Find(k.Musteri_ID);
                                                                    if (m.HesapBakiye >= price)
                                                                    {
                                                                        m.HesapBakiye -= price;
                                                                        pm.Bakiye += price;
                                                                        db.SaveChanges();
                                                                        return "999";
                                                                    }
                                                                    else
                                                                    {
                                                                        return "888";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    return "777";
                                                                }
                                                            }
                                                            else
                                                            {
                                                                return "302";
                                                            }
                                                        }
                                                        else
                                                        {
                                                            return "301";
                                                        }
                                                    }
                                                    else
                                                    {
                                                        return "300";
                                                    }
                                                }
                                                else
                                                {
                                                    return "203";
                                                }
                                            }
                                            else
                                            {
                                                return "202";
                                            }
                                        }
                                        else
                                        {
                                            return "201";
                                        }
                                    }
                                    else
                                    {
                                        return "200";
                                    }
                                }
                                else
                                {
                                    return "505";
                                }
                            }
                            else
                            {
                                return "504";
                            }
                        }
                        else
                        {
                            return "503";
                        }
                    }
                    else
                    {
                        return "502";
                    }
                }
                else
                {
                    return "501";
                }
            }
            else
            {
                return "500";
            }
        }
    }
}