<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EduCorePro.Login" %>

<!DOCTYPE html>
<html lang="tr">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>Giriş Yap / Kayıt Ol - EduCorePro</title>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/css/bootstrap.min.css" rel="stylesheet">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <style>
        body {
            background: linear-gradient(135deg, #1a2238 0%, #3a4b7c 100%);
            min-height: 100vh;
            display: flex;
            flex-direction: column; 
        }
        .navbar-custom {
            background-color: rgba(26, 34, 56, 0.8); 
            backdrop-filter: blur(10px);
            width: 100%;
        }
        .content-wrapper {
            flex-grow: 1; 
            display: flex;
            align-items: center; 
            justify-content: center; 
            width: 100%;
            padding: 20px;
        }
        .auth-card {
            background: white;
            border-radius: 15px;
            box-shadow: 0 10px 25px rgba(0,0,0,0.2);
            overflow: hidden;
            width: 100%;
            max-width: 450px;
            padding: 30px;
        }
        .nav-pills .nav-link {
            color: #555;
            font-weight: bold;
            border-radius: 30px;
            padding: 10px 20px;
            width: 100%;
            text-align: center;
        }
        .nav-pills .nav-link.active {
            background-color: #1a2238;
            color: white;
        }
        .form-control {
            border-radius: 10px;
            padding: 12px;
        }
        .btn-custom {
            background-color: #1a2238;
            color: white;
            border-radius: 10px;
            padding: 12px;
            font-weight: bold;
            width: 100%;
        }

        .btn-custom:hover {
            background-color: #2c3e50;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="display: flex; flex-direction: column; height: 100vh;">
        
        <nav class="navbar navbar-expand-lg navbar-dark navbar-custom py-3">
            <div class="container">
                <a class="navbar-brand fw-bold fs-4" href="Courses.aspx">
                    <i class="fas fa-graduation-cap me-2"></i>EduCorePro
                </a>
                <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#navbarNav">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collapse navbar-collapse" id="navbarNav">
                    <ul class="navbar-nav ms-auto">
                        <li class="nav-item">
                        </li>
                    </ul>
                </div>
            </div>
        </nav>

        <div class="content-wrapper">
            <div class="auth-card">
                <h3 class="text-center fw-bold mb-4" style="color: #1a2238;"><i class="fas fa-user-circle me-2"></i>Giriş Paneli</h3>
                
                <ul class="nav nav-pills nav-fill mb-4" id="pills-tab" role="tablist">
                    <li class="nav-item" role="presentation">
                        <button class="nav-link active" id="pills-login-tab" data-bs-toggle="pill" data-bs-target="#pills-login" type="button">Giriş Yap</button>
                    </li>
                    <li class="nav-item" role="presentation">
                        <button class="nav-link" id="pills-register-tab" data-bs-toggle="pill" data-bs-target="#pills-register" type="button">Kayıt Ol</button>
                    </li>
                </ul>

                <asp:Label ID="lblMesaj" runat="server" Visible="false" CssClass="alert alert-danger d-block text-center mb-3"></asp:Label>

                <div class="tab-content" id="pills-tabContent">
                    
                    <div class="tab-pane fade show active" id="pills-login">
                        <div class="mb-3">
                            <label class="form-label text-muted">E-posta Adresi</label>
                            <div class="input-group">
                                <span class="input-group-text bg-light"><i class="fas fa-envelope"></i></span>
                                <asp:TextBox ID="txtGirisEmail" runat="server" CssClass="form-control" placeholder="ornek@email.com"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mb-4">
                            <label class="form-label text-muted">Şifre</label>
                            <div class="input-group">
                                <span class="input-group-text bg-light"><i class="fas fa-lock"></i></span>
                                <asp:TextBox ID="txtGirisSifre" runat="server" CssClass="form-control" TextMode="Password" placeholder="******"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btnGirisYap" runat="server" Text="Giriş Yap" CssClass="btn btn-custom" OnClick="btnGirisYap_Click" />
                    </div>

                    <div class="tab-pane fade" id="pills-register">
                        <div class="mb-3">
                            <label class="form-label text-muted">E-posta Adresi</label>
                            <div class="input-group">
                                <span class="input-group-text bg-light"><i class="fas fa-envelope"></i></span>
                                <asp:TextBox ID="txtKayitEmail" runat="server" CssClass="form-control" placeholder="ornek@email.com"></asp:TextBox>
                            </div>
                        </div>
                        <div class="mb-4">
                            <label class="form-label text-muted">Şifre Belirle</label>
                            <div class="input-group">
                                <span class="input-group-text bg-light"><i class="fas fa-key"></i></span>
                                <asp:TextBox ID="txtKayitSifre" runat="server" CssClass="form-control" TextMode="Password" placeholder="******"></asp:TextBox>
                            </div>
                        </div>
                        <asp:Button ID="btnKayitOl" runat="server" Text="Hesap Oluştur" CssClass="btn btn-success w-100 py-2 fw-bold" OnClick="btnKayitOl_Click" />
                    </div>
                </div>
            </div>
        </div>

    </form>
    
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0/dist/js/bootstrap.bundle.min.js"></script>
</body>
</html>