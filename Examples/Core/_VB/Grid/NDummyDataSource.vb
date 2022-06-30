Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports Nevron.Nov.Data

Namespace Nevron.Nov.Examples.Grid
    Public Enum ENGender
        Male
        Female
    End Enum

    Public Enum ENCountry
        USA
        UK
        Germany
        France
        Russia
        Italy
        Canada
        China
        Japan
        India
    End Enum

    Public Enum ENJobTitle
        President
        VicePresident
        SalesManager
        SalesRepresentative
        LeadDevelop
        SeniorDeveloper
        JuniorDeveloper
    End Enum

    ''' <summary>
    ''' A static class for creating dummy data sources used in the grid examples.
    ''' </summary>
    Public Module NDummyDataSource
        Private dataTable As Nevron.Nov.Data.NDataTable

        ''' <summary>
        ''' 
        ''' </summary>
        ''' <returns></returns>
        Public Function CreateCompanySalesDataSource() As Nevron.Nov.Data.NDataSource
            Nevron.Nov.Examples.Grid.NDummyDataSource.dataTable = New Nevron.Nov.Data.NMemoryDataTable("CompanySales", New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("Id", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("Company", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Sales", GetType(System.[Double])), New Nevron.Nov.Data.NFieldInfo("Profit", GetType(System.[Double]))})

            For i As Integer = 0 To 50000 - 1
                Call Nevron.Nov.Examples.Grid.NDummyDataSource.dataTable.AddRow(i, Nevron.Nov.Examples.Grid.NDummyDataSource.RandomCompanyName(), Nevron.Nov.Examples.Grid.NDummyDataSource.RandomDouble(500, 5000), Nevron.Nov.Examples.Grid.NDummyDataSource.RandomDouble(100, 1000))
            Next

            Return New Nevron.Nov.Data.NDataSource(Nevron.Nov.Examples.Grid.NDummyDataSource.dataTable)
        End Function
        ''' <summary>
        ''' Creates a products data source.
        ''' </summary>
        Public Function CreateProductsDataSource() As Nevron.Nov.Data.NDataSource
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable("Products", New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("Id", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("Name", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Price", GetType(System.[Double]))})

            For i As Integer = 0 To Nevron.Nov.Examples.Grid.NDummyDataSource.ProductInfos.Length - 1
                Dim productInfo As Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo = Nevron.Nov.Examples.Grid.NDummyDataSource.ProductInfos(i)
                dataTable.AddRow(i, productInfo.Name, productInfo.Price)
            Next

            Return New Nevron.Nov.Data.NDataSource(dataTable)
        End Function
        ''' <summary>
        ''' Creates a persons data source.
        ''' </summary>
        Public Function CreatePersonsDataSource() As Nevron.Nov.Data.NDataSource
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable("Persons", New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("Id", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("Name", GetType(System.[String]), True), New Nevron.Nov.Data.NFieldInfo("Gender", GetType(Nevron.Nov.Examples.Grid.ENGender), True), New Nevron.Nov.Data.NFieldInfo("Birthday", GetType(System.DateTime), True), New Nevron.Nov.Data.NFieldInfo("Country", GetType(Nevron.Nov.Examples.Grid.ENCountry), True), New Nevron.Nov.Data.NFieldInfo("Phone", GetType(System.[String]), True), New Nevron.Nov.Data.NFieldInfo("Email", GetType(System.[String]), True)})

            For i As Integer = 0 To Nevron.Nov.Examples.Grid.NDummyDataSource.PersonInfos.Length - 1
                Dim info As Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo = Nevron.Nov.Examples.Grid.NDummyDataSource.PersonInfos(i)
                dataTable.AddRow(i, info.Name, info.Gender, info.Birthday, info.Country, info.Phone, info.Email)              ' id
      ' name
    ' gender
  ' birthday
   ' country
     ' pnone
      ' email
            Next

            Return New Nevron.Nov.Data.NDataSource(dataTable)
        End Function
        ''' <summary>
        ''' Creates a persons order data source.
        ''' </summary>
        Public Function CreatePersonsOrdersDataSource() As Nevron.Nov.Data.NDataSource
            Dim dataTable As Nevron.Nov.Data.NMemoryDataTable = New Nevron.Nov.Data.NMemoryDataTable("PersonsOrders", New Nevron.Nov.Data.NFieldInfo() {New Nevron.Nov.Data.NFieldInfo("PersonId", GetType(System.Int32)), New Nevron.Nov.Data.NFieldInfo("Product Name", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Product Name1", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Ship To Country", GetType(Nevron.Nov.Examples.Grid.ENCountry)), New Nevron.Nov.Data.NFieldInfo("Ship To Country1", GetType(Nevron.Nov.Examples.Grid.ENCountry)), New Nevron.Nov.Data.NFieldInfo("Ship To City", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Ship To City1", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Ship To Address", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Ship To Address1", GetType(System.[String])), New Nevron.Nov.Data.NFieldInfo("Price", GetType(System.[Double])), New Nevron.Nov.Data.NFieldInfo("Price1", GetType(System.[Double])), New Nevron.Nov.Data.NFieldInfo("Quantity", GetType(System.Int32))})

            For i As Integer = 0 To 10000 - 1
                Dim addressInfo As Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo = Nevron.Nov.Examples.Grid.NDummyDataSource.RandomAddressInfo()
                Dim productInfo As Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo = Nevron.Nov.Examples.Grid.NDummyDataSource.RandomProductInfo()
                dataTable.AddRow(Nevron.Nov.Examples.Grid.NDummyDataSource.RandomPersonIndex(), productInfo.Name, productInfo.Name, addressInfo.Country, addressInfo.Country, addressInfo.City, addressInfo.City, addressInfo.Address, addressInfo.Address, productInfo.Price, productInfo.Price, Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](5) + 1)    ' person id
       ' product name
       ' product name
    ' ship to country 
    ' ship to country 
       ' ship to city
       ' ship to city
    ' ship to address
    ' ship to address
      ' price
      ' price
      ' quantity
            Next

            Return New Nevron.Nov.Data.NDataSource(dataTable)
        End Function

        #Region"Static Fields"

        Private Random As System.Random = New System.Random()

        #EndRegion

        #Region"Numbers"

        ''' <summary>
        ''' Creates a random double in the specified range.
        ''' </summary>
        ''' <paramname="min"></param>
        ''' <paramname="max"></param>
        ''' <returns></returns>
        Public Function RandomDouble(ByVal min As Integer, ByVal max As Integer) As Double
            Dim range As Integer = max - min
            Dim value As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](range)
            Return min + value
        End Function
        ''' <summary>
        ''' Creates a random Int32 in the specified range.
        ''' </summary>
        ''' <paramname="min"></param>
        ''' <paramname="max"></param>
        ''' <returns></returns>
        Public Function RandomInt32(ByVal min As Integer, ByVal max As Integer) As Integer
            Dim range As Integer = max - min
            Dim value As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](range)
            Return min + value
        End Function

        #EndRegion

        #Region"Company Names"

        ''' <summary>
        ''' Picks a random Company Name from the CompanyNames array.
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomCompanyName() As String
            Dim count As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.CompanyNames.Length
            Dim index As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](count)
            Return Nevron.Nov.Examples.Grid.NDummyDataSource.CompanyNames(Nevron.Nov.NMath.Clamp(0, count - 1, index))
        End Function
        ''' <summary>
        ''' List of dummy company names.
        ''' </summary>
        Public CompanyNames As String() = New String() {"Feugiat Metus Foundation", "Ligula Consectetuer Foundation", "Non LLC", "Tincidunt Institute", "Sollicitudin A Malesuada Industries", "Ornare In Faucibus Industries", "In Faucibus Industries", "Iaculis Aliquet Diam Company", "Porttitor Interdum LLC", "Egestas Duis Ltd", "Vel Turpis Aliquam LLC", "Ullamcorper Velit Associates", "Arcu Associates", "Sapien Aenean Massa PC", "Convallis Dolor Quisque PC", "A Ultricies Adipiscing Corp.", "Enim Corporation", "Luctus Sit Amet Consulting", "Eu PC", "Dui Nec Urna PC", "Pede Nonummy Consulting", "Vestibulum Mauris Company", "Consequat Ltd", "Mi Lacinia Mattis LLC", "Ultrices Vivamus Rhoncus PC", "Nec Ante Blandit LLP", "Sollicitudin Commodo Ipsum Corp.", "Diam Company", "Orci Corporation", "Tristique Pharetra Corporation", "Blandit Nam Limited", "Magna A Consulting", "Commodo Auctor Velit Ltd", "Donec Corporation", "Est Nunc Ullamcorper Foundation", "Accumsan Industries", "Ullamcorper Viverra LLC", "Ornare Ltd", "Pharetra Ltd", "Praesent Eu Institute", "Orci Tincidunt Incorporated", "Urna Vivamus Molestie Institute", "Ante Bibendum Company", "Aliquam Enim Nec Company", "Sem PC", "Euismod Est Limited", "Orci Lobortis Augue PC", "Curabitur Massa Vestibulum Institute", "Velit LLP", "Risus Foundation", "Sed Industries", "Dignissim Tempor Company", "Facilisis Corporation", "Aenean Massa Company", "Arcu Imperdiet Corp.", "Aliquam Corporation", "Nulla Facilisi Sed Associates", "Aliquam Arcu Aliquam Associates", "Ligula Incorporated", "Egestas Urna Justo Limited", "At Ltd", "Vivamus Sit Amet Corporation", "Lacus Aliquam Rutrum Company", "Enim Sit LLC", "Turpis Ltd", "Etiam Bibendum Fermentum Company", "Ultrices Iaculis Incorporated", "Penatibus Et Magnis Incorporated", "Aliquam Rutrum Industries", "Fringilla LLP", "Nonummy Fusce Corporation", "Nibh Vulputate Mauris Incorporated", "Praesent PC", "Lorem Ut Aliquam Consulting", "Est Incorporated", "Nisl Maecenas Corp.", "Lorem Sit Amet Ltd", "Lorem Company", "Risus Donec Egestas Corporation", "Sit Corporation", "Vulputate Associates", "Imperdiet Dictum Ltd", "Et Magna Praesent Incorporated", "Sem Eget LLP", "Rutrum Eu Incorporated", "Lorem Consulting", "Nisl Sem Consequat Industries", "Cubilia Curae; Industries", "Enim Corp.", "Arcu Corp.", "Suspendisse Tristique Neque Corp.", "Ut LLP", "Condimentum Donec Industries", "Aliquet Vel Vulputate Limited", "Imperdiet Institute", "Dictum Limited", "Duis Limited", "Augue Ac LLC", "Condimentum Donec Limited", "Consequat Company"}

        #EndRegion

        #Region"Person Infos"

        ''' <summary>
        ''' Picks a random Index in the PersonInfos array.
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomPersonIndex() As Integer
            Dim count As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.PersonInfos.Length
            Dim index As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](count)
            Return Nevron.Nov.NMath.Clamp(0, count - 1, index)
        End Function
        ''' <summary>
        ''' Picks a random NPersonInfo in the PersonInfos array.
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomPersonInfo() As Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo
            Return Nevron.Nov.Examples.Grid.NDummyDataSource.PersonInfos(Nevron.Nov.Examples.Grid.NDummyDataSource.RandomPersonIndex())
        End Function
        ''' <summary>
        ''' List of dummy person names.
        ''' </summary>
        Public PersonInfos As Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo() = New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo() {New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Jinny Collazo", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("John Duke", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Kellie Ferrell", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Sibyl Woosley", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Kourtney Mattoon", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Bruce Fail", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Dario Karl", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Aliza Sermons", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Sung Trout", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Barb Stiner", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Ashlie Bynum", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Carola Saeed", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Brunilda Hermanson", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Lois Capel", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Jerome Moody", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Booker Quach", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Malcolm Luckett", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Mana Snapp", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Georgianna Leung", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Romana Gentle", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Garfield Tranmer", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Rossana Heintzelman", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Carmina Vanorden", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Roger Patten", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Cleopatra Morrill", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Tammara Cumberbatch", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Vita Trinh", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Evangeline Aguinaldo", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Angelo Arzola", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Reynaldo Manahan", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Luis Peoples", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Sheena Fritsch", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Isaiah Key", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Sunny Vath", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Isidro Monsen", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Mariko Ek", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Jessenia Northup", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Cordie Lesage", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Henrietta Fuentes", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Han Snover", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Alesia Pearman", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Mai Czapla", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Maryam Torgersen", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Ken Vaca", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Martina Matson", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Blanche Desrochers", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Rosie Griffing", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Nona Mccroskey", Nevron.Nov.Examples.Grid.ENGender.Female), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Arnold Mayen", Nevron.Nov.Examples.Grid.ENGender.Male), New Nevron.Nov.Examples.Grid.NDummyDataSource.NPersonInfo("Yulanda Wenner", Nevron.Nov.Examples.Grid.ENGender.Female)}

        ''' <summary>
        ''' Represents a fictional person information.
        ''' </summary>
        Public Class NPersonInfo
            Public Sub New(ByVal name As String, ByVal gender As Nevron.Nov.Examples.Grid.ENGender)
                Me.Name = name
                Me.Gender = gender

                ' random birthday of 20-50 year olds
                Dim days As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](30 * 365) + 20 * 365
                Me.Birthday = System.DateTime.Now - New System.TimeSpan(days, 0, 0, 0)

                ' random country
                Me.Country = CType(Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](Nevron.Nov.NEnum.GetValues(CType((GetType(Nevron.Nov.Examples.Grid.ENCountry)), System.Type)).Length), Nevron.Nov.Examples.Grid.ENCountry)

                ' random phone
                Dim areaCode As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](999)
                Dim firstPhonePart As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](999)
                Dim secondPhonePart As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](9999)
                Me.Phone = System.[String].Format("({0})-{1}-{2}", areaCode.ToString("000"), firstPhonePart.ToString("000"), secondPhonePart.ToString("0000"))

                ' email
                Me.Email = Me.Name.ToLower().Replace(" ", ".") & "@domain.com"
            End Sub

            Public Name As String
            Public Gender As Nevron.Nov.Examples.Grid.ENGender
            Public Birthday As System.DateTime
            Public Country As Nevron.Nov.Examples.Grid.ENCountry
            Public Phone As String
            Public Email As String
        End Class

        #EndRegion

        #Region"Product Names"

        ''' <summary>
        ''' Picks a random Product Name from the ProductNames array.
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomProductInfo() As Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo
            Return Nevron.Nov.Examples.Grid.NDummyDataSource.ProductInfos(Nevron.Nov.Examples.Grid.NDummyDataSource.RandomProductIndex())
        End Function
        ''' <summary>
        ''' Picks a random Index
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomProductIndex() As Integer
            Dim count As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.ProductInfos.Length
            Dim index As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](count)
            Return Nevron.Nov.NMath.Clamp(0, count - 1, index)
        End Function
        ''' <summary>
        ''' List of dummy product names.
        ''' </summary>
        Public ProductInfos As Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo() = New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo() {New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Dingtincof"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Conremdox"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Sonlight"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Vivatom"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Trans Tax"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Super Matdox"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Geodomflex"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Re Zootop"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Hot-Lam"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Single-Top"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Tranhome"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Gravenimhold"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Zun Zimtam"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Ancore"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Zun Keydom"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Ontodex"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Freshdox"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Yearair"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Lam-Dox"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Toughcom"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Zoo Ing"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Aptax"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Statfan"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Joykix"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Indigolam"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NProductInfo("Hattom")}
        ''' <summary>
        ''' Represents a fictional product
        ''' </summary>
        Public Class NProductInfo
            Public Sub New(ByVal name As String)
                Me.Name = name
                Me.Price = (CDbl(Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](8000)) + 2000) / 100
            End Sub

            Public Name As String
            Public Price As Double
        End Class

        #EndRegion

        #Region"Address Info"

        ''' <summary>
        ''' Picks a random NAddressInfo AddressInfo from the AddressInfos array
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomAddressInfo() As Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo
            Return Nevron.Nov.Examples.Grid.NDummyDataSource.AddressInfos(Nevron.Nov.Examples.Grid.NDummyDataSource.RandomAddressIndex())
        End Function
        ''' <summary>
        ''' Picks a random index in the AddressInfos array
        ''' </summary>
        ''' <returns></returns>
        Public Function RandomAddressIndex() As Integer
            Dim count As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.AddressInfos.Length
            Dim index As Integer = Nevron.Nov.Examples.Grid.NDummyDataSource.Random.[Next](count)
            Return Nevron.Nov.NMath.Clamp(0, count - 1, index)
        End Function
        ''' <summary>
        ''' Array of fictional addresses
        ''' </summary>
                    ' USA
            
            ' UK
            
            ' Germany
            
            ' France
            
            ' Russia
            
            ' Italy
            
            ' Canada
            
            ' China
            
            ' Japan
                        
            ' India
            Public AddressInfos As Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo() = New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo() {New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "New York", "7414 Park Place"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "New York", "1394 Bayberry Drive"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "New York", "9436 Parker Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Los Angeles", "2101 William Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Los Angeles", "5073 Eagle Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Los Angeles", "439 Atlantic Avenue"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Chicago", "245 Beech Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Chicago", "420 Hamilton Road"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.USA, "Chicago", "540 Maple Lane "), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "London", "854 Lawrence Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "London", "13 Park Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "London", "461 Front Street North"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Birmingham", "281 4th Street North"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Birmingham", "49 Division Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Birmingham", "848 8th Street South"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Leeds", "334 Catherine Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Leeds", "885 Grant Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.UK, "Leeds", "468 Main Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Berlin", "Fischerinsel 81"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Berlin", "Budapester Strasse 14"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Berlin", "Knesebeckstrasse 1"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Hamburg", "Gotzkowskystrasse 74"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Hamburg", "Prenzlauer Allee 42"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Hamburg", "Am Borsigturm 60"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Munich", "An der Schillingbrucke 98"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Munich", "Rankestraße 2"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Germany, "Munich", "An der Schillingbrucke 97"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Paris", "87, Rue de Strasbourg"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Paris", "73, place Stanislas"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Paris", "67, avenue Ferdinand de Lesseps"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Marseille", "30, rue Gontier-Patin"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Marseille", "65, rue Gontier-Patin"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Marseille", "20, rue Grande Fusterie"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Lyon", "57, Rue St Ferréol"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Lyon", "25, boulevard de Prague"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.France, "Lyon", "25, Avenue des Tuileries"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Moscow", "ul. Podgorska 67"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Moscow", "ul. Boleslawa 63"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Moscow", "ul. Dukielska 105"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Saint Petersburg", "ul. Uri Lubelsky 2"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Saint Petersburg", "ul. Tehniku 23"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Saint Petersburg", "ul. Ana Kolska 89"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Novosibirsk", "ul. Sobranie 45"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Novosibirsk", "ul. Pobeda 68"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Russia, "Novosibirsk", "ul. Kolarski Les 46"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Rome", "Via Santa Teresa degli Scalzi, 55"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Rome", "Via Agostino Depretis, 110"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Rome", "Via Firenze, 59"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Milan", "Via Torricelli, 137"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Milan", "Via Nazario Sauro, 125"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Milan", "Via Zannoni, 19"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Naples", "Via del Caggio, 55"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Naples", "Corso Porta Borsari, 71"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Italy, "Naples", "Via Santa Maria di Costantinopoli, 55"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Toronto", "1696 Heritage Drive"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Toronto", "592 Sturgeon Drive"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Toronto", "2188 York St"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Montreal", "4875 Bloor Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Montreal", "2458 Hammarskjold Dr"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Montreal", "3384 James Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Calgary", "4454 rue Saint-Édouard"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Calgary", "628 Merivale Road"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Canada, "Calgary", "4748 Exmouth Street"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Shanghai", "No. 1234, 1241, Yun He Zhen Jie Fang Lu"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Shanghai", "No. 3452, 1983, 1013, Nan Yuan Lu"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Shanghai", "No. 8985, 1028, Feng Cun Wang Bo Zi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Beijing", "No. 1034, 1179, Lin Jiang San Cun"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Beijing", "No. 1781, 1212, Fu Xing Hou Jie"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Beijing", "No. 1462, 1143, Qun Li Xiang"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Tianjin", "No. 1896, 1278, Bei Gang Gong Ye Lou"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Tianjin", "No. 1174, 1035, Xu Cheng Zhen Dui Bao Xiang"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.China, "Tianjin", "No. 1979, 1045, Nan Shen Gou"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Tokyo", "293-1086, Kozukayamate, Tarumi-ku Kobe-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Tokyo", "493-1081, Takanodaiminami, Sugito-machi Kitakatsushika-gun"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Tokyo", "137-1285, Mizunaka, Takayama-mura Kamitakai-gun"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Yokohama", "185-1227, Mizuho, Hanamigawa-ku Chiba-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Yokohama", "233-1273, Shimogamo Kodonocho, Sakyo-ku Kyoto-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Yokohama", "396-1162, Nakata, Noheji-machi Kamikita-gun"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Osaka", "230-1058, Honen, Shirakawa-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Osaka", "234-1267, Sechibarucho Akakoba, Sasebo-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.Japan, "Osaka", "189-1273, Yorozuyamachi, Nagasaki-shi"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Bombay", "Kismat Nagar, Kurla West, Mumba, 173021"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Bombay", "Friends Colony, Hallow Pul, Kurla West, 573201"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Bombay", "Hallow Pul, Kurla West, 273001"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Calcutta", "Maulana Ishaque Street, Ar Rashidiyyah, General Ganj, 733051"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Calcutta", "Ar Rashidiyyah, General Ganj, Kanpur, 567825"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Calcutta", "General Ganj, Kanpur, Uttar Pradesh, 837547"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Delhi", "George Town Rd, George Town, Allahabad, 273001"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Delhi", "Hubli - Gadag Rd, Railway Colony, 123456"), New Nevron.Nov.Examples.Grid.NDummyDataSource.NAddressInfo(Nevron.Nov.Examples.Grid.ENCountry.India, "Delhi", "Akshaibar Singh Marg, Golghar, Gorakhpur, 223152")}
        ''' <summary>
        ''' Represents a fictional address.
        ''' </summary>
        Public Class NAddressInfo
            Public Sub New(ByVal country As Nevron.Nov.Examples.Grid.ENCountry, ByVal city As String, ByVal address As String)
                Me.Country = country
                Me.City = city
                Me.Address = address
            End Sub

            Public Country As Nevron.Nov.Examples.Grid.ENCountry
            Public City As String
            Public Address As String
        End Class

        #EndRegion
    End Module
End Namespace
