using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISIP123_Solodov_WPF
{
    partial class  basepart
    {
        public string PrintInfo
        {
            get
            {
                switch (parttypeid)
                {
                    case 1:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Сокет {cpu.socket.name}\n" +
                            $"Число ядер: {cpu.numberofcores} шт\n" +
                            $"Базовая частота: {cpu.basecorefrequency} ГГц\n" +
                            $"Максимальная частота: {cpu.maxcorefrequency} ГГц\n" +
                            $"Кэш l3: {cpu.cachel3} МБ\n" +
                            $"Встроеная gpu: {cpu.igpu.name}\n" +
                            $"Тепловая мощность: {cpu.thermalpower} Вт";
                    case 2:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Интерфейс: {gpu.gpuinterface.name}\n" +
                            $"Частота чипа: {gpu.chipfrequency} МГц\n" +
                            $"Видеопамять: {gpu.videomemory} Гб\n" +
                            $"Шина памяти: {gpu.memorybus} бит\n" +
                            $"Рекомендуемая мощность: {gpu.recommendpower} W";
                    case 3:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Тип памяти: {ram.memorytype.name}\n" +
                            $"Объём: {ram.capacity} Гб\n" +
                            $"Количество: {ram.count} шт\n" +
                            $"Тактовая частота: {ram.ghz} МГц\n" +
                            $"Тайминги: {ram.timings}";
                    case 4:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Сокет: {motherboard.socket.name}\n" +
                            $"Формфактор: {motherboard.formfactor.name}\n" +
                            $"Слоты памяти: {motherboard.memoryslots} шт\n" +
                            $"Тип памяти: {motherboard.memorytype.name}\n" +
                            $"Cлоты pci: {motherboard.pcislots} шт\n" +
                            $"Порты sata: {motherboard.sataports} шт\n" +
                            $"Порты usb: {motherboard.usbports} шт";
                    case 5:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Размер корпуса: {@case.casesize.name}\n" +
                            $"Слоты расширения: {@case.expansionslots} шт\n" +
                            $"Вентиляторы: {@case.fans} шт";
                    case 6:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Мощность: {powersupply.power} W\n" +
                            $"Размер вентилятора: {powersupply.fandimension.name}\n" +
                            $"Сертификат: {powersupply.certificate.name}";
                    case 7:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Размер вентилятора: {processorcooler.fandimension.name}\n" +
                            $"Тепловые трубки: {processorcooler.heatpipes} шт\n" +
                            $"Минимальная скорость: {processorcooler.minspeed} об/мин\n" +
                            $"Максимальная скорость: {processorcooler.maxspeed} об/мин\n" +
                            $"Уровень шума: {processorcooler.noiselevel} дБ";
                    case 8:
                        return $"Название: {name}\n" +
                            $"Цена: {price}₽\n" +
                            $"Объём: {storagedevice.capacity} МБ\n" +
                            $"Тип памяти: {storagedevice.storagedevicetype.name}\n" +
                            $"Интерфейс: {storagedevice.storagedeviceinterface.name}";
                    default:
                        return "Ы";
                }
            }
        }


    }
}
