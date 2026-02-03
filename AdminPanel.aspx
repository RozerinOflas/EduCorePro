<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="EduCorePro.AdminPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .table img { width: 50px; height: 50px; object-fit: cover; border-radius: 5px; }
       
        body.dark-mode .table { --bs-table-bg: #2c2c2c; --bs-table-color: #fff; border-color: #555; }
        body.dark-mode .table thead th, body.dark-mode .table th { background-color: #1a1a1a !important; color: #ffffff !important; border-bottom: 2px solid #777 !important; font-weight: bold; }
        body.dark-mode .table tbody tr:hover { color: #fff; background-color: #333; }
        body.dark-mode .table td { color: #e0e0e0 !important; border-color: #555 !important; }
        body.dark-mode .form-control, body.dark-mode .form-select { background-color: #333 !important; border: 1px solid #666 !important; color: #ffffff !important; }
        body.dark-mode .form-control::placeholder { color: #ccc !important; opacity: 1; }
        body.dark-mode .card { background-color: #1e1e1e !important; border: 1px solid #444; }
        body.dark-mode .card-body.bg-light { background-color: #1e1e1e !important; color: #fff !important; }
        body.dark-mode .nav-tabs .nav-link.active { background-color: #1e1e1e; color: #fff; border-color: #444 #444 #1e1e1e; }
        body.dark-mode .nav-tabs .nav-link { color: #aaa; }
        body.dark-mode h5, body.dark-mode h6, body.dark-mode label { color: #ffffff !important; }
        body.dark-mode .text-muted { color: #bbb !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h2 class="fw-bold"><i class="fas fa-cogs me-2"></i>Yönetim Paneli</h2>
            <asp:Label ID="lblMesaj" runat="server" Visible="false"></asp:Label>
        </div>

        <ul class="nav nav-tabs" id="adminTabs" role="tablist">
            <li class="nav-item">
                <button class="nav-link active fw-bold" id="courses-tab" data-bs-toggle="tab" data-bs-target="#courses" type="button">Kurs Yönetimi</button>
            </li>
            <li class="nav-item">
                <button class="nav-link fw-bold" id="users-tab" data-bs-toggle="tab" data-bs-target="#users" type="button">Kullanıcılar ve Raporlar</button>
            </li>
        </ul>

        <div class="tab-content p-4 border border-top-0 bg-white rounded-bottom shadow-sm" id="myTabContent" style="min-height: 500px;">
            
            <div class="tab-pane fade show active" id="courses" role="tabpanel">
                
                <div class="card mb-4 bg-light border-0">
                    <div class="card-body">
                        <h5 class="fw-bold mb-3"><i class="fas fa-plus-circle text-success me-2"></i>Kurs Ekle / Düzenle</h5>
                        <asp:HiddenField ID="hfKursId" runat="server" />
                        <div class="row g-3">
                            <div class="col-md-4">
                                <asp:TextBox ID="txtBaslik" runat="server" CssClass="form-control" placeholder="Kurs Başlığı" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-2">
                                <asp:TextBox ID="txtFiyat" runat="server" CssClass="form-control" placeholder="Fiyat (TL)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlKategori" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Kategori Seçin" Value="" />
                                    <asp:ListItem Text="Yazılım" Value="Yazilim" />
                                    <asp:ListItem Text="Veritabanı" Value="Veritabani" />
                                    <asp:ListItem Text="Dil Eğitimi" Value="Dil" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-3">
                                <asp:DropDownList ID="ddlEgitmen" runat="server" CssClass="form-select"></asp:DropDownList>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtResim" runat="server" CssClass="form-control" placeholder="Resim URL (Örn: Images/csharp.jpg)" autocomplete="off"></asp:TextBox>
                            </div>
                            <div class="col-md-6">
                                <asp:TextBox ID="txtVideoUrl" runat="server" CssClass="form-control" placeholder="Video Linki" autocomplete="off"></asp:TextBox>
                            </div>
                            
                            <div class="col-12 text-end">
                                <asp:Button ID="btnVazgec" runat="server" Text="Vazgeç" CssClass="btn btn-secondary me-2" OnClick="btnVazgec_Click" Visible="false" />
                                <asp:Button ID="btnGuncelle" runat="server" Text="Güncelle" CssClass="btn btn-warning fw-bold me-2" OnClick="btnGuncelle_Click" Visible="false" />
                                <asp:Button ID="btnKursEkle" runat="server" Text="Kursu Kaydet" CssClass="btn btn-success fw-bold" OnClick="btnKursEkle_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <h5 class="fw-bold mt-4">Mevcut Kurs Listesi</h5>
                <div class="table-responsive">
                    <asp:GridView ID="gridCourses" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-middle" 
                        OnRowDeleting="gridCourses_RowDeleting" OnSelectedIndexChanged="gridCourses_SelectedIndexChanged" 
                        DataKeyNames="Id" GridLines="None">
                        <Columns>
                            <asp:TemplateField HeaderText="Resim">
                                <ItemTemplate><img src='<%# Eval("Image") %>' alt="img" /></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Title" HeaderText="Başlık" />
                            <asp:BoundField DataField="Category" HeaderText="Kategori" />
                            <asp:BoundField DataField="Price" HeaderText="Fiyat" DataFormatString="{0:C}" />
                            <asp:CommandField ShowSelectButton="True" SelectText="<i class='fas fa-edit'></i> Düzenle" ButtonType="Link"><ControlStyle CssClass="btn btn-sm btn-outline-primary border-0 fw-bold" /></asp:CommandField>
                            <asp:CommandField ShowDeleteButton="True" DeleteText="<i class='fas fa-trash text-danger'></i>" ButtonType="Link"><ControlStyle CssClass="btn btn-sm" /></asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <div class="tab-pane fade" id="users" role="tabpanel">
                
                <h5 class="fw-bold mb-3"><i class="fas fa-chart-bar text-primary me-2"></i>Özel Raporlar</h5>

                <div class="card mb-3 border-warning">
                    <div class="card-body bg-light">
                        <h6 class="fw-bold text-dark"><i class="fas fa-link me-2"></i>Özel İşlem: Inner Join </h6>
                        <div class="d-flex align-items-center flex-wrap">
                            <span class="me-3 text-muted">Kurslar tablosu ile Eğitmenler tablosunu birleştirip getir:</span>
                            <asp:Button ID="btnInnerJoin" runat="server" Text="Eğitmen Analizi Raporu" 
                                CssClass="btn btn-warning text-dark fw-bold btn-sm" OnClick="btnInnerJoin_Click" />
                        </div>
                        <asp:GridView ID="gridInnerJoin" runat="server" AutoGenerateColumns="true" 
                            CssClass="table table-bordered table-striped mt-3 bg-white" Visible="false">
                        </asp:GridView>
                    </div>
                </div>

                <div class="card mb-4 border-info">
                    <div class="card-body bg-light">
                        <h6 class="fw-bold text-info"><i class="fas fa-filter me-2"></i>Özel Rapor: 3 Dereceli İç İçe Sorgu</h6>
                        <div class="row g-2 align-items-center">
                            <div class="col-auto"><span class="fw-bold">Kategori:</span></div>
                            <div class="col-auto">
                                <asp:DropDownList ID="ddlRaporKategori" runat="server" CssClass="form-select">
                                    <asp:ListItem Text="Yazılım" Value="Yazilim" />
                                    <asp:ListItem Text="Veritabanı" Value="Veritabani" />
                                    <asp:ListItem Text="Dil Eğitimi" Value="Dil" />
                                </asp:DropDownList>
                            </div>
                            <div class="col-auto">
                                <asp:Button ID="btnRaporGetir" runat="server" Text="Kategori Raporu" 
                                    CssClass="btn btn-info text-white fw-bold" OnClick="btnRaporGetir_Click" />
                            </div>
                            <div class="col-auto">
                                <asp:Button ID="btnTumunuGetir" runat="server" Text="Temizle" 
                                    CssClass="btn btn-secondary" OnClick="btnTumunuGetir_Click" Visible="false" />
                            </div>
                        </div>
                        
                        <div class="mt-3">
                             <asp:GridView ID="gridReport" runat="server" AutoGenerateColumns="true" 
                                CssClass="table table-bordered table-striped bg-white">
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <h5 class="fw-bold mt-4"><i class="fas fa-users text-primary me-2"></i>Kayıtlı Kullanıcı Listesi</h5>
                <div class="table-responsive">
                    <asp:GridView ID="gridUsers" runat="server" AutoGenerateColumns="False" CssClass="table table-hover align-middle"
                        OnRowDeleting="gridUsers_RowDeleting" DataKeyNames="Id" GridLines="None">
                        <Columns>
                            <asp:BoundField DataField="Id" HeaderText="ID" />
                            <asp:BoundField DataField="Email" HeaderText="E-posta" />
                            <asp:BoundField DataField="Role" HeaderText="Rol" />
                            <asp:CommandField ShowDeleteButton="True" DeleteText="<i class='fas fa-trash text-danger'></i> Sil" ButtonType="Link"><ControlStyle CssClass="btn btn-sm btn-outline-danger border-0" /></asp:CommandField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
    </div>

    <script>
        document.addEventListener("DOMContentLoaded", function () {
            var activeTabId = localStorage.getItem("adminActiveTab");
            if (activeTabId) {
                var tabTrigger = document.querySelector(activeTabId);
                if (tabTrigger) { var tab = new bootstrap.Tab(tabTrigger); tab.show(); }
            }
            var tabElements = document.querySelectorAll('button[data-bs-toggle="tab"]');
            tabElements.forEach(function (el) {
                el.addEventListener('shown.bs.tab', function (event) {
                    localStorage.setItem("adminActiveTab", "#" + event.target.id);
                });
            });
            function applyDarkToTabs() {
                var body = document.body;
                var content = document.getElementById("myTabContent");
                if (body.classList.contains("dark-mode")) {
                    content.classList.remove("bg-white");
                    content.style.backgroundColor = "#1e1e1e";
                    content.style.color = "white";
                }
            }
            applyDarkToTabs();
        });
    </script>
</asp:Content>