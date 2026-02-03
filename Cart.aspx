<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Cart.aspx.cs" Inherits="EduCorePro.Cart" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-5">
        <h2 class="fw-bold mb-4"><i class="fas fa-shopping-cart text-primary me-2"></i>Alışveriş Sepetim</h2>

        <asp:Label ID="lblBosSepet" runat="server" Visible="false" CssClass="alert alert-warning d-block text-center fw-bold">
            Sepetinizde ürün bulunmamaktadır.
        </asp:Label>

        <div class="row">
            <div class="col-lg-8">
                <asp:Repeater ID="rptSepet" runat="server" OnItemCommand="rptSepet_ItemCommand">
                    <ItemTemplate>
                        <div class="card mb-3 shadow-sm border-0">
                            <div class="row g-0 align-items-center">
                                <div class="col-md-2 p-2">
                                    <img src='<%# Eval("Image") %>' class="img-fluid rounded-3" alt="kurs" style="width:100%; height:80px; object-fit:cover;">
                                </div>
                                <div class="col-md-8">
                                    <div class="card-body">
                                        <h5 class="card-title fw-bold mb-1"><%# Eval("Title") %></h5>
                                        <p class="card-text text-muted small mb-0"><%# Eval("Category") %></p>
                                    </div>
                                </div>
                                <div class="col-md-2 text-center">
                                    <h5 class="fw-bold text-primary mb-2"><%# Eval("Price", "{0:C}") %></h5>
                                    
                                    <asp:LinkButton ID="btnSil" runat="server" CommandName="Sil" CommandArgument='<%# Eval("Id") %>' 
                                        CssClass="btn btn-outline-danger btn-sm border-0" OnClientClick="return confirm('Bu kursu sepetten çıkarmak istiyor musunuz?');">
                                        <i class="fas fa-trash-alt me-1"></i> Sil
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>

            <div class="col-lg-4">
                <asp:Panel ID="pnlSepetOzet" runat="server" CssClass="card shadow-sm border-0 bg-light">
                    <div class="card-body p-4">
                        <h5 class="fw-bold mb-4">Sipariş Özeti</h5>
                        <div class="d-flex justify-content-between mb-3">
                            <span class="text-muted">Ara Toplam</span>
                            <span class="fw-bold"><asp:Label ID="lblToplamTutar" runat="server"></asp:Label> TL</span>
                        </div>
                        <hr />
                        <div class="d-grid gap-2">
                            <asp:Button ID="btnSepetiOnayla" runat="server" Text="Sepeti Onayla ve Bitir" 
                                CssClass="btn btn-primary btn-lg fw-bold" OnClick="btnSepetiOnayla_Click" />
                            
                            <a href="Courses.aspx" class="btn btn-outline-secondary">Alışverişe Devam Et</a>
                        </div>
                        <div class="mt-3 text-center small text-muted">
                            <i class="fas fa-lock me-1"></i> Ödeme bilgileriniz 256-bit SSL ile korunmaktadır.
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>

</asp:Content>