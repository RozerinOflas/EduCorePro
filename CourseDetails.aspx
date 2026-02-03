<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseDetails.aspx.cs" Inherits="EduCorePro.CourseDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .accordion-button:not(.collapsed) { color: #0c63e4; background-color: #e7f1ff; }
        
        body.dark-mode .accordion-item { 
            background-color: #2c2c2c !important; 
            border: 1px solid #444 !important; 
        }
        body.dark-mode .accordion-button { 
            background-color: #2c2c2c !important; 
            color: #fff !important; 
            box-shadow: none !important; 
        }
        body.dark-mode .accordion-button:not(.collapsed) {
            background-color: #333 !important;
            color: #ffc107 !important;
            box-shadow: none !important;
        }
        body.dark-mode .accordion-body { 
            background-color: #1e1e1e !important; 
            color: #ffffff !important; 
        }
        body.dark-mode .list-group-item {
            background-color: transparent !important; 
            color: #ffffff !important; 
            border-color: #444 !important;
        }
        body.dark-mode .btn-outline-dark { 
            color: #fff !important; 
            border-color: #fff !important; 
        }
        body.dark-mode .btn-outline-dark:hover { 
            background-color: #fff !important; 
            color: #000 !important; 
        }

        body.dark-mode .alert-info {
            background-color: #0c2e44 !important;
            color: #b6d4fe !important;
            border: 1px solid #084298 !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row mt-4">
        <div class="col-lg-8">
            <div class="mb-4">
                <h1 class="fw-bold display-5 mb-2"><asp:Label ID="lblTitle" runat="server"></asp:Label></h1>
                <p class="fs-5 text-muted">
                    <span class="badge bg-primary me-2"><asp:Label ID="lblCategory" runat="server"></asp:Label></span>
                    <span><i class="fas fa-chalkboard-teacher me-2"></i><asp:Label ID="lblInstructor" runat="server"></asp:Label></span>
                </p>
            </div>

            <div class="card shadow-sm border-0 mb-4 p-4 rounded-4">
                <h4 class="fw-bold mb-3"><i class="fas fa-info-circle text-primary me-2"></i>Kurs Hakkında</h4>
                <p class="lead" style="font-size: 1.1rem; line-height: 1.7;"><asp:Label ID="lblDescription" runat="server"></asp:Label></p>
            </div>

            <div class="card shadow-sm border-0 mb-4 p-4 rounded-4">
                <h4 class="fw-bold mb-3"><i class="fas fa-check-circle text-success me-2"></i>Neler Öğreneceksiniz?</h4>
                <div class="row">
                    <div class="col-md-6"><ul class="list-unstyled">
                        <li><i class="fas fa-check text-success me-2"></i>Temel ve İleri Seviye Bilgiler</li>
                        <li><i class="fas fa-check text-success me-2"></i>Pratik Uygulamalar ve Örnekler</li>
                    </ul></div>
                    <div class="col-md-6"><ul class="list-unstyled">
                        <li><i class="fas fa-check text-success me-2"></i>Kapsamlı Müfredat İçeriği</li>
                        <li><i class="fas fa-check text-success me-2"></i>Uzman Eğitmen Desteği</li>
                    </ul></div>
                </div>
            </div>

            <h4 class="fw-bold mb-3 mt-5">Kurs İçeriği</h4>
            <div class="accordion mb-5" id="accordionExample">
                <asp:Repeater ID="rptDersler" runat="server">
                    <ItemTemplate>
                        <div class="accordion-item">
                            <h2 class="accordion-header">
                                <button class="accordion-button collapsed fw-bold" type="button" data-bs-toggle="collapse" data-bs-target="#collapse<%# Container.ItemIndex %>">
                                    Bölüm <%# Container.ItemIndex + 1 %>: <%# Eval("Baslik") %>
                                </button>
                            </h2>
                            <div id="collapse<%# Container.ItemIndex %>" class="accordion-collapse collapse" data-bs-parent="#accordionExample">
                                <div class="accordion-body">
                                    <ul class="list-group list-group-flush">
                                        <li class="list-group-item bg-transparent"><i class="fas fa-play-circle me-2 text-primary"></i><%# Eval("Icerik1") %> (15 dk)</li>
                                        <li class="list-group-item bg-transparent"><i class="fas fa-file-alt me-2 text-warning"></i><%# Eval("Icerik2") %> (Okuma)</li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="alert alert-info border-0 shadow-sm rounded-4 mb-5">
                <h5 class="fw-bold"><i class="fas fa-exclamation-circle me-2"></i>Gereksinimler</h5>
                <ul class="mb-0">
                    <li>İnternet bağlantısı olan bir bilgisayar veya telefon.</li>
                    <li>Öğrenme isteği ve motivasyon.</li>
                    <li>Hiçbir ön bilgiye gerek yoktur, sıfırdan başlanır.</li>
                </ul>
            </div>
        </div>

        <div class="col-lg-4">
            <div class="card shadow border-0 rounded-4 p-3 sticky-top" style="top: 100px; z-index: 1;">
                <div class="ratio ratio-16x9 mb-3 rounded-3 overflow-hidden shadow-sm">
                    <iframe id="videoPlayer" runat="server" src="" title="YouTube video" allowfullscreen></iframe>
                </div>
                <h2 class="fw-bold mb-3 display-6"><asp:Label ID="lblPrice" runat="server"></asp:Label> TL</h2>
                <asp:Label ID="lblMesaj" runat="server" Visible="false"></asp:Label>
                
                <div class="d-grid gap-2 mt-2">
                    
                    <asp:Label ID="lblAdminUyari" runat="server" Visible="false" CssClass="alert alert-warning text-center fw-bold d-block mb-2">
                        <i class="fas fa-user-shield me-2"></i> Yönetici hesapları kurs satın alamaz.
                    </asp:Label>

                    <asp:Button ID="btnSepeteEkle" runat="server" Text="Sepete Ekle" CssClass="btn btn-warning btn-lg fw-bold text-dark" OnClick="btnSepeteEkle_Click" />
                    <asp:Button ID="btnSatinAl" runat="server" Text="Hemen Satın Al" CssClass="btn btn-outline-dark fw-bold py-2" OnClick="btnSatinAl_Click" />
                </div>

                <div class="text-center mt-4 small text-muted">
                    <p class="mb-1"><i class="fas fa-check-circle text-success me-1"></i> 30 Gün Para İade Garantisi</p>
                    <p class="mb-1"><i class="fas fa-infinity me-1"></i> Ömür Boyu Erişim</p>
                    <p><i class="fas fa-certificate text-warning me-1"></i> Bitirme Sertifikası</p>
                </div>
            </div>
        </div>
    </div>
</asp:Content>