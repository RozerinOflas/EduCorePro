<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="courses.aspx.cs" Inherits="EduCorePro.Kurslar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"></asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center mb-5 py-4 rounded-3" style="background-color: #f8f9fa;">
        <h2 class="fw-bold display-5" style="color: #1a2238;">Geleceğini Şekillendir</h2>
        <p class="text-muted fs-5">En popüler yazılım ve kariyer eğitimlerini keşfet.</p>
    </div>

    <div class="row">
        <asp:Repeater ID="rptKurslar" runat="server">
            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card h-100 shadow-sm border-0">
                        <div class="position-relative">
                            <a href="CourseDetails.aspx?id=<%# Eval("Id") %>">
                                <img src="<%# Eval("Image") %>" class="card-img-top" style="height: 180px; object-fit: cover;">
                            </a>
                            <span class="badge bg-primary position-absolute top-0 start-0 m-2"><%# Eval("Category") %></span>
                        </div>
                        <div class="card-body">
                            <h5 class="card-title fw-bold text-truncate"><%# Eval("Title") %></h5>
                            <p class="text-muted small mb-2"><i class="fas fa-chalkboard-teacher me-1"></i><%# Eval("InstructorName") %></p>
                            <h5 class="text-primary fw-bold"><%# Eval("Price") %> TL</h5>
                        </div>
                        <div class="card-footer bg-transparent border-0 pb-3">
                            <a href="CourseDetails.aspx?id=<%# Eval("Id") %>" class="btn btn-outline-primary w-100 fw-bold">İncele</a>
                        </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        
        <asp:Panel ID="pnlNoData" runat="server" Visible="false" CssClass="text-center w-100 mt-4">
            <div class="alert alert-warning">
                <h4 class="alert-heading"><i class="fas fa-exclamation-triangle me-2"></i>Sonuç Bulunamadı</h4>
                <p>Aradığınız kriterlere uygun kurs yok.</p>
                <a href="courses.aspx" class="btn btn-dark">Tüm Kursları Göster</a>
            </div>
        </asp:Panel>
    </div>

    <div class="container-fluid py-5 mt-5 rounded-4" style="background-color: #e9ecef;">
        <div class="container">
            <h2 class="text-center fw-bold mb-5" style="color: #1a2238;">Öğrencilerimiz Ne Diyor?</h2>
            
            <div id="commentCarousel" class="carousel slide carousel-dark" data-bs-ride="carousel">
                <div class="carousel-inner">
                    
                    <div class="carousel-item active">
                        <div class="text-center mx-auto" style="max-width: 700px;">
                            <i class="fas fa-quote-left fa-3x text-warning mb-3"></i>
                            <p class="fs-4 fst-italic mb-4">
                                "Bu platform sayesinde yazılıma olan korkumu yendim. Eğitmenlerin anlatımı muazzam. 
                                Projelerle öğrenmek gerçekten çok etkiliymiş. Herkese tavsiye ederim!"
                            </p>
                            <div class="d-flex justify-content-center align-items-center mt-3">
                                <div class="bg-white rounded-circle d-flex align-items-center justify-content-center shadow-sm" style="width: 60px; height: 60px;">
                                    <i class="fas fa-user text-primary fs-3"></i>
                                </div>
                                <div class="text-start ms-3">
                                    <h5 class="fw-bold mb-0">Ahmet Yılmaz</h5>
                                    <small class="text-muted">Bilgisayar Müh. Öğrencisi</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="carousel-item">
                        <div class="text-center mx-auto" style="max-width: 700px;">
                            <i class="fas fa-quote-left fa-3x text-warning mb-3"></i>
                            <p class="fs-4 fst-italic mb-4">
                                "Kariyer değişikliği yapmak istiyordum. EduCorePro'daki SQL eğitimi ile veri analitiği alanında 
                                kendimi çok geliştirdim. Teşekkürler!"
                            </p>
                            <div class="d-flex justify-content-center align-items-center mt-3">
                                <div class="bg-white rounded-circle d-flex align-items-center justify-content-center shadow-sm" style="width: 60px; height: 60px;">
                                    <i class="fas fa-user text-danger fs-3"></i>
                                </div>
                                <div class="text-start ms-3">
                                    <h5 class="fw-bold mb-0">Zeynep Kaya</h5>
                                    <small class="text-muted">Veri Analisti</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="carousel-item">
                        <div class="text-center mx-auto" style="max-width: 700px;">
                            <i class="fas fa-quote-left fa-3x text-warning mb-3"></i>
                            <p class="fs-4 fst-italic mb-4">
                                "Hem fiyatlar uygun hem de içerikler çok kaliteli. İngilizce kursunu aldım, 
                                pratik yapma imkanları çok iyi. Sertifikamı aldım bile!"
                            </p>
                            <div class="d-flex justify-content-center align-items-center mt-3">
                                <div class="bg-white rounded-circle d-flex align-items-center justify-content-center shadow-sm" style="width: 60px; height: 60px;">
                                    <i class="fas fa-user text-success fs-3"></i>
                                </div>
                                <div class="text-start ms-3">
                                    <h5 class="fw-bold mb-0">Mehmet Demir</h5>
                                    <small class="text-muted">Freelance Geliştirici</small>
                                </div>
                            </div>
                        </div>
                    </div>

                </div>
                
                <button class="carousel-control-prev" type="button" data-bs-target="#commentCarousel" data-bs-slide="prev">
                    <span class="carousel-control-prev-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Önceki</span>
                </button>
                <button class="carousel-control-next" type="button" data-bs-target="#commentCarousel" data-bs-slide="next">
                    <span class="carousel-control-next-icon" aria-hidden="true"></span>
                    <span class="visually-hidden">Sonraki</span>
                </button>
            </div>
        </div>
    </div>

</asp:Content>