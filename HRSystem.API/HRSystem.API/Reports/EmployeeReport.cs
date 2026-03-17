using HRSystem.Application.DTOs.Employee;
using HRSystem.Application.DTOs.Vacation;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace HRSystem.API.Reports
{
    public class EmployeeReport : IDocument
    {
        private readonly EmployeeResponseDto _employee;
        private readonly IEnumerable<VacationResponseDto> _vacations;

        public EmployeeReport(EmployeeResponseDto employee, IEnumerable<VacationResponseDto> vacations)
        {
            _employee = employee;
            _vacations = vacations;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.DefaultTextStyle(x => x.FontSize(12));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                {
                    x.Span("صفحة ");
                    x.CurrentPageNumber();
                    x.Span(" من ");
                    x.TotalPages();
                });
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text("نظام إدارة الموظفين والإجازات")
                        .FontSize(20).Bold().FontColor("#1a3c5e");
                    col.Item().Text("تقرير بيانات الموظف")
                        .FontSize(12).FontColor("#475569");
                });
                row.ConstantItem(100).Height(50)
                    .Background("#1a3c5e")
                    .AlignCenter().AlignMiddle()
                    .Text("HR").FontSize(24).Bold().FontColor("#ffffff");
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.Column(col =>
            {
                col.Spacing(15);

                // بيانات الموظف
                col.Item().Text("بيانات الموظف").FontSize(14).Bold().FontColor("#1a3c5e");

                col.Item().Border(1).BorderColor("#cbd5e1").Padding(10).Column(info =>
                {
                    info.Item().Row(row =>
                    {
                        row.RelativeItem().Text("رقم الموظف:").Bold();
                        row.RelativeItem().Text(_employee.EmployeeCode);
                        row.RelativeItem().Text("الاسم:").Bold();
                        row.RelativeItem().Text(_employee.FullName);
                    });
                    info.Item().Row(row =>
                    {
                        row.RelativeItem().Text("تاريخ الميلاد:").Bold();
                        row.RelativeItem().Text(_employee.BirthDate?.ToString() ?? "—");
                        row.RelativeItem().Text("المؤهل:").Bold();
                        row.RelativeItem().Text(_employee.Qualification ?? "—");
                    });
                    info.Item().Row(row =>
                    {
                        row.RelativeItem().Text("إجمالي أيام الإجازات:").Bold();
                        row.RelativeItem().Text($"{_employee.TotalVacationDays} يوم");
                        row.RelativeItem();
                        row.RelativeItem();
                    });
                });

                // جدول الإجازات
                col.Item().Text("سجل الإجازات").FontSize(14).Bold().FontColor("#1a3c5e");

                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(cols =>
                    {
                        cols.ConstantColumn(30);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(2);
                        cols.RelativeColumn(1);
                    });

                    // Header
                    table.Header(header =>
                    {
                        header.Cell().Background("#1a3c5e").Padding(5)
                            .Text("#").FontColor("#ffffff").Bold();
                        header.Cell().Background("#1a3c5e").Padding(5)
                            .Text("نوع الإجازة").FontColor("#ffffff").Bold();
                        header.Cell().Background("#1a3c5e").Padding(5)
                            .Text("تاريخ البداية").FontColor("#ffffff").Bold();
                        header.Cell().Background("#1a3c5e").Padding(5)
                            .Text("تاريخ النهاية").FontColor("#ffffff").Bold();
                        header.Cell().Background("#1a3c5e").Padding(5)
                            .Text("المدة").FontColor("#ffffff").Bold();
                    });

                    // Rows
                    var vacList = _vacations.ToList();
                    if (vacList.Count == 0)
                    {
                        table.Cell().ColumnSpan(5).Padding(10)
                            .AlignCenter().Text("لا توجد إجازات مسجلة").FontColor("#94a3b8");
                    }
                    else
                    {
                        for (int i = 0; i < vacList.Count; i++)
                        {
                            var v = vacList[i];
                            var bg = i % 2 == 0 ? "#f8fafc" : "#ffffff";

                            table.Cell().Background(bg).Padding(5).Text((i + 1).ToString());
                            table.Cell().Background(bg).Padding(5).Text(v.VacationType);
                            table.Cell().Background(bg).Padding(5).Text(v.StartDate);
                            table.Cell().Background(bg).Padding(5).Text(v.EndDate);
                            table.Cell().Background(bg).Padding(5).Text($"{v.DurationDays} يوم");
                        }
                    }
                });

                // Date
                col.Item().AlignLeft()
                    .Text($"تاريخ الطباعة: {DateTime.Now:yyyy-MM-dd HH:mm}")
                    .FontSize(10).FontColor("#94a3b8");
            });
        }
    }
}